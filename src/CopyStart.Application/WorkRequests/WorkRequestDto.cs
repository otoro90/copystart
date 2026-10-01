using CopyStart.Domain.Entities;
using CopyStart.Domain.Enums;

namespace CopyStart.Application.WorkRequests;

public record WorkRequestDto(
    Guid Id,
    string TrackingNumber,
    Guid CustomerId,
    Guid? AssetId,
    string Status,
    string Description,
    string ContactName,
    string ContactPhone,
    DateTimeOffset CreatedAt,
    string? AssignedToUserId,
    DateTimeOffset? AssignedAt,
    DateTimeOffset? CompletedAt,
    DateTimeOffset? CancelledAt,
    string? CancellationReason,
    IReadOnlyList<TimelineEventDto> Timeline)
{
    public static WorkRequestDto FromDomain(WorkRequest entity) => new(
        entity.Id,
        entity.TrackingNumber,
        entity.CustomerId,
        entity.AssetId,
        entity.Status.ToLegacyString(),
        entity.Description,
        entity.ContactName,
        entity.ContactPhone,
        entity.CreatedAt,
        entity.AssignedToUserId,
        entity.AssignedAt,
        entity.CompletedAt,
        entity.CancelledAt,
        entity.CancellationReason,
        entity.Timeline.Select(TimelineEventDto.FromDomain).ToList());
}

public record TimelineEventDto(
    Guid Id,
    DateTimeOffset Timestamp,
    string Actor,
    string EventType,
    string Description,
    string? Metadata)
{
    public static TimelineEventDto FromDomain(TimelineEvent entity) => new(
        entity.Id,
        entity.Timestamp,
        entity.Actor,
        entity.EventType,
        entity.Description,
        entity.Metadata);
}
