using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CopyStart.Api.Controllers.V1;
using CopyStart.Application.WorkOrders;
using CopyStart.Application.WorkRequests;
using Xunit;

namespace CopyStart.E2ETests;

public class ApiTests : IClassFixture<CopyStartApiFixture>
{
    private readonly HttpClient _client;

    public ApiTests(CopyStartApiFixture fixture)
    {
        _client = fixture.CreateClient();
    }

    [Fact]
    public async Task UnauthenticatedRequest_Returns401Unauthorized_FailClosed()
    {
        // Act: No X-Test-Actor header provided
        var response = await _client.GetAsync("/api/v1/work-requests");

        // Assert: Fail-closed until ZITADEL
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task LivenessHealthChecks_Return200OK()
    {
        var liveResponse = await _client.GetAsync("/health/live");
        Assert.Equal(HttpStatusCode.OK, liveResponse.StatusCode);

        var healthzResponse = await _client.GetAsync("/healthz");
        Assert.Equal(HttpStatusCode.OK, healthzResponse.StatusCode);
    }

    [Fact]
    public async Task ReadinessHealthChecks_Return503ServiceUnavailable_WhilePersistenceIsFrozen()
    {
        var readyResponse = await _client.GetAsync("/health/ready");
        Assert.Equal(HttpStatusCode.ServiceUnavailable, readyResponse.StatusCode);
        var content = await readyResponse.Content.ReadAsStringAsync();
        Assert.Contains("Unhealthy", content);

        var shortReadyResponse = await _client.GetAsync("/ready");
        Assert.Equal(HttpStatusCode.ServiceUnavailable, shortReadyResponse.StatusCode);
    }

    [Fact]
    public async Task OpenApi_Returns200OK_WithDocument()
    {
        var response = await _client.GetAsync("/openapi/v1.json");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var json = await response.Content.ReadAsStringAsync();
        Assert.Contains("openapi", json);
    }

    [Fact]
    public async Task CreateWorkRequest_FailsValidation_WhenDescriptionIsEmpty()
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/work-requests");
        request.Headers.Add("X-Test-Actor", "staff");
        request.Content = JsonContent.Create(new
        {
            CustomerId = Guid.NewGuid(),
            Description = "",
            ContactName = "Test"
        });

        var response = await _client.SendAsync(request);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);

        var body = await response.Content.ReadAsStringAsync();
        Assert.Contains("Validation Failed", body);
        Assert.Contains("Description is required", body);
    }

    [Fact]
    public async Task WorkRequest_And_WorkOrder_EndToEndFlow_WithAtomicInvalidTransitionCheck()
    {
        // 1. Create a work request without asset
        var customerId = Guid.NewGuid();
        var createRequestMsg = new HttpRequestMessage(HttpMethod.Post, "/api/v1/work-requests");
        createRequestMsg.Headers.Add("X-Test-Actor", "staff");
        createRequestMsg.Content = JsonContent.Create(new CreateWorkRequestCommand(
            CustomerId: customerId,
            Description: "Repair toner feed assembly",
            ContactName: "Marcos V.",
            ContactPhone: "3001234567",
            AssetId: null, // No synthetic asset required
            Actor: "staff"));

        var createRequestRes = await _client.SendAsync(createRequestMsg);
        Assert.Equal(HttpStatusCode.Created, createRequestRes.StatusCode);
        var createdRequest = await createRequestRes.Content.ReadFromJsonAsync<WorkRequestDto>();
        Assert.NotNull(createdRequest);
        Assert.Equal("Por tramitar", createdRequest.Status);
        Assert.Null(createdRequest.AssetId);

        // 2. Assign request to technician
        var assignMsg = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/work-requests/{createdRequest.Id}/assign");
        assignMsg.Headers.Add("X-Test-Actor", "staff");
        assignMsg.Content = JsonContent.Create(new AssignRequestPayload("tech-99", "supervisor"));
        var assignRes = await _client.SendAsync(assignMsg);
        Assert.Equal(HttpStatusCode.OK, assignRes.StatusCode);
        var assignedRequest = await assignRes.Content.ReadFromJsonAsync<WorkRequestDto>();
        Assert.NotNull(assignedRequest);
        Assert.Equal("Asignada", assignedRequest.Status);
        Assert.Equal("tech-99", assignedRequest.AssignedToUserId);

        // 3. Start service
        var startServiceMsg = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/work-requests/{createdRequest.Id}/start-service");
        startServiceMsg.Headers.Add("X-Test-Actor", "staff");
        var startServiceRes = await _client.SendAsync(startServiceMsg);
        Assert.Equal(HttpStatusCode.OK, startServiceRes.StatusCode);
        var inServiceRequest = await startServiceRes.Content.ReadFromJsonAsync<WorkRequestDto>();
        Assert.NotNull(inServiceRequest);
        Assert.Equal("En servicio", inServiceRequest.Status);

        // 4. Create work order for this request
        var createOrderMsg = new HttpRequestMessage(HttpMethod.Post, "/api/v1/work-orders");
        createOrderMsg.Headers.Add("X-Test-Actor", "staff");
        createOrderMsg.Content = JsonContent.Create(new CreateWorkOrderCommand(
            WorkRequestId: createdRequest.Id,
            TechnicianUserId: "tech-99",
            DiagnosticNotes: "Initial inspection shows broken roller clips",
            Actor: "staff"));
        var createOrderRes = await _client.SendAsync(createOrderMsg);
        Assert.Equal(HttpStatusCode.Created, createOrderRes.StatusCode);
        var createdOrder = await createOrderRes.Content.ReadFromJsonAsync<WorkOrderDto>();
        Assert.NotNull(createdOrder);
        Assert.Equal("Por confirmar", createdOrder.Status);

        // 5. Start work order execution
        var startOrderMsg = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/work-orders/{createdOrder.Id}/start");
        startOrderMsg.Headers.Add("X-Test-Actor", "staff");
        var startOrderRes = await _client.SendAsync(startOrderMsg);
        Assert.Equal(HttpStatusCode.OK, startOrderRes.StatusCode);
        var inProgressOrder = await startOrderRes.Content.ReadFromJsonAsync<WorkOrderDto>();
        Assert.NotNull(inProgressOrder);
        Assert.Equal("En ejecucion", inProgressOrder.Status);

        // 6. Complete work order
        var completeOrderMsg = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/work-orders/{createdOrder.Id}/complete");
        completeOrderMsg.Headers.Add("X-Test-Actor", "staff");
        completeOrderMsg.Content = JsonContent.Create(new CompleteOrderPayload("Replaced roller clips and verified feed operation.", "tech-99"));
        var completeOrderRes = await _client.SendAsync(completeOrderMsg);
        Assert.Equal(HttpStatusCode.OK, completeOrderRes.StatusCode);
        var completedOrder = await completeOrderRes.Content.ReadFromJsonAsync<WorkOrderDto>();
        Assert.NotNull(completedOrder);
        Assert.Equal("Finalizado", completedOrder.Status);
        var timelineCountBeforeInvalidAttempt = completedOrder.Timeline.Count;

        // 7. Complete request
        var completeRequestMsg = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/work-requests/{createdRequest.Id}/complete");
        completeRequestMsg.Headers.Add("X-Test-Actor", "staff");
        var completeRequestRes = await _client.SendAsync(completeRequestMsg);
        Assert.Equal(HttpStatusCode.OK, completeRequestRes.StatusCode);
        var completedRequest = await completeRequestRes.Content.ReadFromJsonAsync<WorkRequestDto>();
        Assert.NotNull(completedRequest);
        Assert.Equal("Servicios Finalizados", completedRequest.Status);

        // 8. ATOMIC INVARIANT CHECK:
        // Una orden Finalizado que vuelva al estado inicial falla con error de dominio, sin cambio de estado, sin auditoría y sin Commit.
        var resetOrderMsg = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/work-orders/{createdOrder.Id}/reset");
        resetOrderMsg.Headers.Add("X-Test-Actor", "staff");
        var resetOrderRes = await _client.SendAsync(resetOrderMsg);

        // Fails with documented Problem Details 422 Unprocessable Entity
        Assert.Equal(HttpStatusCode.UnprocessableEntity, resetOrderRes.StatusCode);
        Assert.Equal("application/problem+json", resetOrderRes.Content.Headers.ContentType?.MediaType);

        // Check that state, timeline, and commit were unchanged
        var getOrderMsg = new HttpRequestMessage(HttpMethod.Get, $"/api/v1/work-orders/{createdOrder.Id}");
        getOrderMsg.Headers.Add("X-Test-Actor", "staff");
        var getOrderRes = await _client.SendAsync(getOrderMsg);
        Assert.Equal(HttpStatusCode.OK, getOrderRes.StatusCode);
        var verifiedOrder = await getOrderRes.Content.ReadFromJsonAsync<WorkOrderDto>();
        Assert.NotNull(verifiedOrder);

        // State is still Finalizado
        Assert.Equal("Finalizado", verifiedOrder.Status);
        // Timeline count is unchanged
        Assert.Equal(timelineCountBeforeInvalidAttempt, verifiedOrder.Timeline.Count);
    }
}
