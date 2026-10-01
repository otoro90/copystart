using CopyStart.Application.WorkRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace CopyStart.Api.Controllers.V1;

[ApiController]
[Route("api/v1/work-requests")]
[Authorize]
public class WorkRequestsController : ControllerBase
{
    private readonly IMessageBus _bus;

    public WorkRequestsController(IMessageBus bus)
    {
        _bus = bus;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<WorkRequestDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(CancellationToken ct)
    {
        var result = await _bus.InvokeAsync<IReadOnlyList<WorkRequestDto>>(new ListWorkRequestsQuery(), ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(WorkRequestDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _bus.InvokeAsync<WorkRequestDto?>(new GetWorkRequestByIdQuery(id), ct);
        if (result == null)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Work Request Not Found",
                Detail = $"Work request '{id}' was not found."
            });
        }
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(WorkRequestDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateWorkRequestCommand command, CancellationToken ct)
    {
        var result = await _bus.InvokeAsync<WorkRequestDto>(command, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPost("{id:guid}/assign")]
    [ProducesResponseType(typeof(WorkRequestDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Assign(Guid id, [FromBody] AssignRequestPayload payload, CancellationToken ct)
    {
        var command = new AssignWorkRequestCommand(id, payload.TechnicianUserId, payload.Actor ?? "staff");
        var result = await _bus.InvokeAsync<WorkRequestDto>(command, ct);
        return Ok(result);
    }

    [HttpPost("{id:guid}/start-service")]
    [ProducesResponseType(typeof(WorkRequestDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> StartService(Guid id, [FromBody] ActorPayload? payload, CancellationToken ct)
    {
        var command = new StartWorkRequestServiceCommand(id, payload?.Actor ?? "staff");
        var result = await _bus.InvokeAsync<WorkRequestDto>(command, ct);
        return Ok(result);
    }

    [HttpPost("{id:guid}/complete")]
    [ProducesResponseType(typeof(WorkRequestDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Complete(Guid id, [FromBody] ActorPayload? payload, CancellationToken ct)
    {
        var command = new CompleteWorkRequestCommand(id, payload?.Actor ?? "staff");
        var result = await _bus.InvokeAsync<WorkRequestDto>(command, ct);
        return Ok(result);
    }

    [HttpPost("{id:guid}/cancel")]
    [ProducesResponseType(typeof(WorkRequestDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Cancel(Guid id, [FromBody] CancelRequestPayload payload, CancellationToken ct)
    {
        var command = new CancelWorkRequestCommand(id, payload.Reason, payload.Actor ?? "staff");
        var result = await _bus.InvokeAsync<WorkRequestDto>(command, ct);
        return Ok(result);
    }
}

public record AssignRequestPayload(string TechnicianUserId, string? Actor = null);
public record ActorPayload(string? Actor = null);
public record CancelRequestPayload(string Reason, string? Actor = null);
