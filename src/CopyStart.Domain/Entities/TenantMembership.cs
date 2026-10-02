using CopyStart.Domain.Common;

namespace CopyStart.Domain.Entities;

public class TenantMembership : IAggregateRoot, ITenantOwned
{
    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }
    public string UserId { get; private set; }
    public string Role { get; private set; }
    public bool IsActive { get; private set; }

    public TenantMembership(Guid tenantId, string userId, string role, bool isActive = true)
    {
        TenantId = tenantId == Guid.Empty ? throw new ArgumentException("Tenant ID cannot be empty.", nameof(tenantId)) : tenantId;
        UserId = string.IsNullOrWhiteSpace(userId) ? throw new ArgumentException("UserId cannot be empty.", nameof(userId)) : userId;
        Role = string.IsNullOrWhiteSpace(role) ? throw new ArgumentException("Role cannot be empty.", nameof(role)) : role;
        Id = Guid.NewGuid();
        IsActive = isActive;
    }

    public void Revoke() => IsActive = false;
}
