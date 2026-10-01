using System.Collections.Concurrent;
using CopyStart.Domain.Entities;
using CopyStart.Domain.Repositories;

namespace CopyStart.Infrastructure.Repositories;

public class InMemoryWorkOrderRepository : IWorkOrderRepository
{
    private readonly ConcurrentDictionary<Guid, WorkOrder> _orders = new();

    public Task<WorkOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _orders.TryGetValue(id, out var order);
        return Task.FromResult(order);
    }

    public Task<WorkOrder?> GetByOrderNumberAsync(string orderNumber, CancellationToken cancellationToken = default)
    {
        var order = _orders.Values.FirstOrDefault(o => string.Equals(o.OrderNumber, orderNumber, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(order);
    }

    public Task<IReadOnlyList<WorkOrder>> ListAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<WorkOrder> list = _orders.Values.OrderByDescending(o => o.CreatedAt).ToList();
        return Task.FromResult(list);
    }

    public Task AddAsync(WorkOrder order, CancellationToken cancellationToken = default)
    {
        _orders[order.Id] = order;
        return Task.CompletedTask;
    }

    public Task UpdateAsync(WorkOrder order, CancellationToken cancellationToken = default)
    {
        _orders[order.Id] = order;
        return Task.CompletedTask;
    }
}
