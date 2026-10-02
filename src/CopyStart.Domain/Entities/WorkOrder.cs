using CopyStart.Domain.Common;
using CopyStart.Domain.Enums;
using CopyStart.Domain.Exceptions;

namespace CopyStart.Domain.Entities;

public class WorkOrder : IAggregateRoot, ITenantOwned
{
    private readonly List<TimelineEvent> _timeline = new();

    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }
    public string OrderNumber { get; private set; }
    public Guid WorkRequestId { get; private set; }
    public string TechnicianUserId { get; private set; }
    public WorkOrderStatus Status { get; private set; }
    public string? DiagnosticNotes { get; private set; }
    public string? ResolutionNotes { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? StartedAt { get; private set; }
    public DateTimeOffset? CompletedAt { get; private set; }

    public IReadOnlyCollection<TimelineEvent> Timeline => _timeline.AsReadOnly();

    private WorkOrder()
    {
        OrderNumber = string.Empty;
        TechnicianUserId = string.Empty;
    }

    public WorkOrder(
        Guid tenantId,
        Guid workRequestId,
        string technicianUserId,
        string? diagnosticNotes = null,
        string actor = "system")
    {
        Id = Guid.NewGuid();
        TenantId = tenantId == Guid.Empty ? throw new ArgumentException("Tenant ID is required.", nameof(tenantId)) : tenantId;
        OrderNumber = $"WO-{DateTimeOffset.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..6].ToUpperInvariant()}";
        WorkRequestId = workRequestId == Guid.Empty ? throw new ArgumentException("Work request ID is required.", nameof(workRequestId)) : workRequestId;
        TechnicianUserId = string.IsNullOrWhiteSpace(technicianUserId) ? throw new ArgumentException("Technician user ID is required.", nameof(technicianUserId)) : technicianUserId;
        Status = WorkOrderStatus.PendingConfirmation;
        DiagnosticNotes = diagnosticNotes;
        CreatedAt = DateTimeOffset.UtcNow;

        _timeline.Add(new TimelineEvent(
            tenantId: TenantId,
            actor: actor,
            eventType: "WorkOrderCreated",
            description: $"Work order created with status '{Status.ToLegacyString()}'.",
            workOrderId: Id,
            workRequestId: workRequestId));
    }

    public void StartExecution(string actor)
    {
        if (Status != WorkOrderStatus.PendingConfirmation)
        {
            throw new InvalidStateTransitionException(nameof(WorkOrder), Status.ToLegacyString(), WorkOrderStatus.InProgress.ToLegacyString());
        }

        Status = WorkOrderStatus.InProgress;
        StartedAt = DateTimeOffset.UtcNow;

        _timeline.Add(new TimelineEvent(
            tenantId: TenantId,
            actor: actor,
            eventType: "WorkOrderStarted",
            description: $"Work order execution started. Status changed to '{Status.ToLegacyString()}'.",
            workOrderId: Id,
            workRequestId: WorkRequestId));
    }

    public void Complete(string resolutionNotes, string actor)
    {
        if (Status != WorkOrderStatus.InProgress)
        {
            throw new InvalidStateTransitionException(nameof(WorkOrder), Status.ToLegacyString(), WorkOrderStatus.Completed.ToLegacyString());
        }

        if (string.IsNullOrWhiteSpace(resolutionNotes))
        {
            throw new ArgumentException("Resolution notes are required to complete a work order.", nameof(resolutionNotes));
        }

        Status = WorkOrderStatus.Completed;
        ResolutionNotes = resolutionNotes;
        CompletedAt = DateTimeOffset.UtcNow;

        _timeline.Add(new TimelineEvent(
            tenantId: TenantId,
            actor: actor,
            eventType: "WorkOrderCompleted",
            description: $"Work order completed. Status changed to '{Status.ToLegacyString()}'.",
            workOrderId: Id,
            workRequestId: WorkRequestId));
    }

    /// <summary>
    /// Explicit check for returning to initial state or invalid transitions.
    /// When an invalid transition is attempted, state remains intact, no timeline event is recorded.
    /// </summary>
    public void ResetToInitialState()
    {
        throw new InvalidStateTransitionException(nameof(WorkOrder), Status.ToLegacyString(), WorkOrderStatus.PendingConfirmation.ToLegacyString());
    }
}
