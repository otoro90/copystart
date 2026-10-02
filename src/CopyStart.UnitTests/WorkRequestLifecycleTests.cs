using CopyStart.Domain.Entities;
using CopyStart.Domain.Enums;
using CopyStart.Domain.Exceptions;
using CopyStart.Domain.Modules.Assets;
using CopyStart.Domain.Modules.Parts;
using CopyStart.Domain.Modules.Procedures;
using Xunit;

namespace CopyStart.UnitTests;

public class WorkRequestLifecycleTests
{
    [Fact]
    public void WorkRequest_CanBeCreated_WithoutSyntheticAsset()
    {
        var customerId = Guid.NewGuid();
        var request = new WorkRequest(
            tenantId: Guid.NewGuid(),
            customerId: customerId,
            description: "General diagnostic and maintenance",
            contactName: "Ana Gómez",
            contactPhone: "3001234567",
            assetId: null, // No asset managed
            actor: "dispatcher");

        Assert.Null(request.AssetId);
        Assert.Equal(customerId, request.CustomerId);
        Assert.Equal(WorkRequestStatus.PendingTriage, request.Status);
        Assert.Equal("Por tramitar", request.Status.ToLegacyString());
        Assert.StartsWith("REQ-", request.TrackingNumber);
        Assert.Single(request.Timeline);
        Assert.Equal("RequestCreated", request.Timeline.First().EventType);
    }

    [Fact]
    public void WorkRequest_FullLifecycle_TransitionsThroughValidStates()
    {
        var request = new WorkRequest(
            tenantId: Guid.NewGuid(),
            customerId: Guid.NewGuid(),
            description: "Replace toner and drum cartridge",
            contactName: "Carlos Pérez",
            contactPhone: "3109876543");

        // 1. PendingTriage -> Assigned
        request.Assign("tech-456", "supervisor");
        Assert.Equal(WorkRequestStatus.Assigned, request.Status);
        Assert.Equal("Asignada", request.Status.ToLegacyString());
        Assert.Equal("tech-456", request.AssignedToUserId);
        Assert.NotNull(request.AssignedAt);

        // 2. Assigned -> InService
        request.StartService("tech-456");
        Assert.Equal(WorkRequestStatus.InService, request.Status);
        Assert.Equal("En servicio", request.Status.ToLegacyString());

        // 3. InService -> Completed
        request.Complete("tech-456");
        Assert.Equal(WorkRequestStatus.Completed, request.Status);
        Assert.Equal("Servicios Finalizados", request.Status.ToLegacyString());
        Assert.NotNull(request.CompletedAt);

        Assert.Equal(4, request.Timeline.Count);
    }

    [Fact]
    public void WorkRequest_CanBeCancelled_WhenActive()
    {
        var request = new WorkRequest(
            tenantId: Guid.NewGuid(),
            customerId: Guid.NewGuid(),
            description: "Calibration check",
            contactName: "Laura R.",
            contactPhone: "3000000000");

        request.Cancel("Client rescheduled service", "supervisor");

        Assert.Equal(WorkRequestStatus.Cancelled, request.Status);
        Assert.Equal("Cancelada", request.Status.ToLegacyString());
        Assert.Equal("Client rescheduled service", request.CancellationReason);
        Assert.NotNull(request.CancelledAt);
    }

    [Fact]
    public void WorkRequest_CannotBeCancelled_OnceCompleted()
    {
        var request = new WorkRequest(
            tenantId: Guid.NewGuid(),
            customerId: Guid.NewGuid(),
            description: "Urgent fix",
            contactName: "Luis M.",
            contactPhone: "3201112233");

        request.Assign("tech-1", "supervisor");
        request.StartService("tech-1");
        request.Complete("tech-1");

        Assert.Throws<InvalidStateTransitionException>(() =>
        {
            request.Cancel("Late cancellation attempt", "supervisor");
        });
    }

    [Fact]
    public void OptionalModules_Asset_Procedure_Part_CanBeInstantiatedIndependently()
    {
        var customerId = Guid.NewGuid();
        var tenantId = Guid.NewGuid();
        var asset = new Asset(tenantId, customerId, "SN-998877", "Canon", "imageRUNNER ADVANCE", "Floor 2 Copy room");
        Assert.Equal("SN-998877", asset.SerialNumber);
        Assert.True(asset.IsActive);

        var procedure = new Procedure(tenantId, "PROC-01", "Standard Drum Replacement", "Step 1: Disconnect power...");
        Assert.Equal("PROC-01", procedure.Code);
        Assert.True(procedure.IsActive);

        var part = new Part(tenantId, "PART-ROLLER-01", "Paper Pickup Roller", "Rubber feed roller", 15.50m, 20);
        Assert.Equal(20, part.AvailableStock);
        part.AdjustStock(-2);
        Assert.Equal(18, part.AvailableStock);
    }

    [Fact]
    public void Customer_And_ServiceCatalog_AreModelledCorrectly()
    {
        var tenantId = Guid.NewGuid();
        var customer = new Customer(tenantId, "Acme Corp", "contact@acme.com", "555-1234", "TAX-900123", "Calle 100 # 15-20");
        Assert.Equal("Acme Corp", customer.Name);
        Assert.True(customer.IsActive);

        var catalogItem = new ServiceCatalogItem(tenantId, "MAINT-PREV", "Preventive Maintenance", "Full cleaning and calibration", 150.00m, TimeSpan.FromHours(2));
        Assert.Equal("MAINT-PREV", catalogItem.Code);
        Assert.Equal(150.00m, catalogItem.BasePrice);
    }
}
