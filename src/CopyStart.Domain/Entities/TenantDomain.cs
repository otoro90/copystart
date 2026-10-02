using CopyStart.Domain.Common;

namespace CopyStart.Domain.Entities;

public class TenantDomain : IAggregateRoot, ITenantOwned
{
    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }
    public string Hostname { get; private set; }
    public bool IsPrimary { get; private set; }
    public bool IsVerified { get; private set; }

    public TenantDomain(Guid tenantId, string hostname, bool isPrimary = false, bool isVerified = true)
    {
        TenantId = tenantId == Guid.Empty ? throw new ArgumentException("Tenant ID cannot be empty.", nameof(tenantId)) : tenantId;
        Hostname = string.IsNullOrWhiteSpace(hostname) ? throw new ArgumentException("Hostname cannot be empty.", nameof(hostname)) : hostname.ToLowerInvariant();
        Id = Guid.NewGuid();
        IsPrimary = isPrimary;
        IsVerified = isVerified;
    }
}
