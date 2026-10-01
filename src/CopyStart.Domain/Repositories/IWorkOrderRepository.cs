using CopyStart.Domain.Entities;

namespace CopyStart.Domain.Repositories;

public interface IWorkOrderRepository
{
    Task<WorkOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<WorkOrder?> GetByOrderNumberAsync(string orderNumber, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<WorkOrder>> ListAsync(CancellationToken cancellationToken = default);
    Task AddAsync(WorkOrder order, CancellationToken cancellationToken = default);
    Task UpdateAsync(WorkOrder order, CancellationToken cancellationToken = default);
}
