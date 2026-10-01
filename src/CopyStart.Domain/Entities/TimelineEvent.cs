namespace CopyStart.Domain.Entities;

public class TimelineEvent
{
    public Guid Id { get; private set; }
    public DateTimeOffset Timestamp { get; private set; }
    public string Actor { get; private set; }
    public string EventType { get; private set; }
    public string Description { get; private set; }
    public string? Metadata { get; private set; }
    public Guid? WorkRequestId { get; private set; }
    public Guid? WorkOrderId { get; private set; }

    private TimelineEvent()
    {
        Actor = string.Empty;
        EventType = string.Empty;
        Description = string.Empty;
    }

    public TimelineEvent(
        string actor,
        string eventType,
        string description,
        Guid? workRequestId = null,
        Guid? workOrderId = null,
        string? metadata = null)
    {
        Id = Guid.NewGuid();
        Timestamp = DateTimeOffset.UtcNow;
        Actor = string.IsNullOrWhiteSpace(actor) ? throw new ArgumentException("Actor is required.", nameof(actor)) : actor;
        EventType = string.IsNullOrWhiteSpace(eventType) ? throw new ArgumentException("EventType is required.", nameof(eventType)) : eventType;
        Description = string.IsNullOrWhiteSpace(description) ? throw new ArgumentException("Description is required.", nameof(description)) : description;
        WorkRequestId = workRequestId;
        WorkOrderId = workOrderId;
        Metadata = metadata;
    }
}
