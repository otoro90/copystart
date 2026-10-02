using CopyStart.Domain.Common;
using CopyStart.Domain.Enums;
using CopyStart.Domain.Exceptions;

namespace CopyStart.Domain.Entities;

public class WorkRequest : IAggregateRoot, ITenantOwned
{
    private readonly List<TimelineEvent> _timeline = new();

    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }
    public string TrackingNumber { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid? AssetId { get; private set; }
    public WorkRequestStatus Status { get; private set; }
    public string Description { get; private set; }
    public string ContactName { get; private set; }
    public string ContactPhone { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public string? AssignedToUserId { get; private set; }
    public DateTimeOffset? AssignedAt { get; private set; }
    public DateTimeOffset? CompletedAt { get; private set; }
    public DateTimeOffset? CancelledAt { get; private set; }
    public string? CancellationReason { get; private set; }

    public IReadOnlyCollection<TimelineEvent> Timeline => _timeline.AsReadOnly();

    private WorkRequest()
    {
        TrackingNumber = string.Empty;
        Description = string.Empty;
        ContactName = string.Empty;
        ContactPhone = string.Empty;
    }

    public WorkRequest(
        Guid tenantId,
        Guid customerId,
        string description,
        string contactName,
        string contactPhone,
        Guid? assetId = null,
        string actor = "system")
    {
        Id = Guid.NewGuid();
        TenantId = tenantId == Guid.Empty ? throw new ArgumentException("Tenant ID is required.", nameof(tenantId)) : tenantId;
        TrackingNumber = $"REQ-{DateTimeOffset.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..6].ToUpperInvariant()}";
        CustomerId = customerId == Guid.Empty ? throw new ArgumentException("Customer ID is required.", nameof(customerId)) : customerId;
        AssetId = assetId; // Nullable: can be created without synthetic asset
        Description = string.IsNullOrWhiteSpace(description) ? throw new ArgumentException("Description is required.", nameof(description)) : description;
        ContactName = string.IsNullOrWhiteSpace(contactName) ? throw new ArgumentException("Contact name is required.", nameof(contactName)) : contactName;
        ContactPhone = contactPhone ?? string.Empty;
        Status = WorkRequestStatus.PendingTriage;
        CreatedAt = DateTimeOffset.UtcNow;

        _timeline.Add(new TimelineEvent(
            tenantId: TenantId,
            actor: actor,
            eventType: "RequestCreated",
            description: $"Work request created with status '{Status.ToLegacyString()}'.",
            workRequestId: Id));
    }

    public void Assign(string technicianUserId, string actor)
    {
        if (Status != WorkRequestStatus.PendingTriage)
        {
            throw new InvalidStateTransitionException(nameof(WorkRequest), Status.ToLegacyString(), WorkRequestStatus.Assigned.ToLegacyString());
        }

        if (string.IsNullOrWhiteSpace(technicianUserId))
        {
            throw new ArgumentException("Technician user ID is required.", nameof(technicianUserId));
        }

        AssignedToUserId = technicianUserId;
        AssignedAt = DateTimeOffset.UtcNow;
        Status = WorkRequestStatus.Assigned;

        _timeline.Add(new TimelineEvent(
            tenantId: TenantId,
            actor: actor,
            eventType: "RequestAssigned",
            description: $"Request assigned to technician '{technicianUserId}'. Status changed to '{Status.ToLegacyString()}'.",
            workRequestId: Id));
    }

    public void StartService(string actor)
    {
        if (Status != WorkRequestStatus.Assigned)
        {
            throw new InvalidStateTransitionException(nameof(WorkRequest), Status.ToLegacyString(), WorkRequestStatus.InService.ToLegacyString());
        }

        Status = WorkRequestStatus.InService;

        _timeline.Add(new TimelineEvent(
            tenantId: TenantId,
            actor: actor,
            eventType: "ServiceStarted",
            description: $"Service started. Status changed to '{Status.ToLegacyString()}'.",
            workRequestId: Id));
    }

    public void Complete(string actor)
    {
        if (Status != WorkRequestStatus.InService)
        {
            throw new InvalidStateTransitionException(nameof(WorkRequest), Status.ToLegacyString(), WorkRequestStatus.Completed.ToLegacyString());
        }

        Status = WorkRequestStatus.Completed;
        CompletedAt = DateTimeOffset.UtcNow;

        _timeline.Add(new TimelineEvent(
            tenantId: TenantId,
            actor: actor,
            eventType: "RequestCompleted",
            description: $"Work request completed. Status changed to '{Status.ToLegacyString()}'.",
            workRequestId: Id));
    }

    public void Cancel(string reason, string actor)
    {
        if (Status is WorkRequestStatus.Completed or WorkRequestStatus.Cancelled)
        {
            throw new InvalidStateTransitionException(nameof(WorkRequest), Status.ToLegacyString(), WorkRequestStatus.Cancelled.ToLegacyString());
        }

        if (string.IsNullOrWhiteSpace(reason))
        {
            throw new ArgumentException("Cancellation reason is required.", nameof(reason));
        }

        Status = WorkRequestStatus.Cancelled;
        CancelledAt = DateTimeOffset.UtcNow;
        CancellationReason = reason;

        _timeline.Add(new TimelineEvent(
            tenantId: TenantId,
            actor: actor,
            eventType: "RequestCancelled",
            description: $"Work request cancelled. Reason: {reason}.",
            workRequestId: Id));
    }
}
