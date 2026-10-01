using CopyStart.Domain.Common;

namespace CopyStart.Infrastructure.Persistence;

public class InMemoryUnitOfWork : IUnitOfWork
{
    public int CommitCount { get; private set; }

    public Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        CommitCount++;
        return Task.FromResult(1);
    }
}
