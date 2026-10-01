using CopyStart.Domain.Entities;

namespace CopyStart.Domain.Repositories;

public interface IWorkRequestRepository
{
    Task<WorkRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<WorkRequest?> GetByTrackingNumberAsync(string trackingNumber, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<WorkRequest>> ListAsync(CancellationToken cancellationToken = default);
    Task AddAsync(WorkRequest request, CancellationToken cancellationToken = default);
    Task UpdateAsync(WorkRequest request, CancellationToken cancellationToken = default);
}
