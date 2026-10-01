using System.Collections.Concurrent;
using CopyStart.Domain.Entities;
using CopyStart.Domain.Repositories;

namespace CopyStart.Infrastructure.Repositories;

public class InMemoryWorkRequestRepository : IWorkRequestRepository
{
    private readonly ConcurrentDictionary<Guid, WorkRequest> _requests = new();

    public Task<WorkRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _requests.TryGetValue(id, out var request);
        return Task.FromResult(request);
    }

    public Task<WorkRequest?> GetByTrackingNumberAsync(string trackingNumber, CancellationToken cancellationToken = default)
    {
        var request = _requests.Values.FirstOrDefault(r => string.Equals(r.TrackingNumber, trackingNumber, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(request);
    }

    public Task<IReadOnlyList<WorkRequest>> ListAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<WorkRequest> list = _requests.Values.OrderByDescending(r => r.CreatedAt).ToList();
        return Task.FromResult(list);
    }

    public Task AddAsync(WorkRequest request, CancellationToken cancellationToken = default)
    {
        _requests[request.Id] = request;
        return Task.CompletedTask;
    }

    public Task UpdateAsync(WorkRequest request, CancellationToken cancellationToken = default)
    {
        _requests[request.Id] = request;
        return Task.CompletedTask;
    }
}
