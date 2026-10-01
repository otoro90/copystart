using System.Collections.Concurrent;
using CopyStart.Domain.Entities;
using CopyStart.Domain.Repositories;

namespace CopyStart.Infrastructure.Repositories;

public class InMemoryCustomerRepository : ICustomerRepository
{
    private readonly ConcurrentDictionary<Guid, Customer> _customers = new();

    public Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _customers.TryGetValue(id, out var customer);
        return Task.FromResult(customer);
    }

    public Task<IReadOnlyList<Customer>> ListAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<Customer> list = _customers.Values.OrderBy(c => c.Name).ToList();
        return Task.FromResult(list);
    }

    public Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        _customers[customer.Id] = customer;
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        _customers[customer.Id] = customer;
        return Task.CompletedTask;
    }
}
