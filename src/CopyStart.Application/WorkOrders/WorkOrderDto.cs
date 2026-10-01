using CopyStart.Application.WorkRequests;
using CopyStart.Domain.Entities;
using CopyStart.Domain.Enums;

namespace CopyStart.Application.WorkOrders;

public record WorkOrderDto(
    Guid Id,
    string OrderNumber,
    Guid WorkRequestId,
    string TechnicianUserId,
    string Status,
    string? DiagnosticNotes,
    string? ResolutionNotes,
    DateTimeOffset CreatedAt,
    DateTimeOffset? StartedAt,
    DateTimeOffset? CompletedAt,
    IReadOnlyList<TimelineEventDto> Timeline)
{
    public static WorkOrderDto FromDomain(WorkOrder entity) => new(
        entity.Id,
        entity.OrderNumber,
        entity.WorkRequestId,
        entity.TechnicianUserId,
        entity.Status.ToLegacyString(),
        entity.DiagnosticNotes,
        entity.ResolutionNotes,
        entity.CreatedAt,
        entity.StartedAt,
        entity.CompletedAt,
        entity.Timeline.Select(TimelineEventDto.FromDomain).ToList());
}
