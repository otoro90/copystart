using CopyStart.Domain.Common;

namespace CopyStart.Domain.Entities;

public class ServiceCatalogItem : IAggregateRoot, ITenantOwned
{
    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal BasePrice { get; private set; }
    public TimeSpan EstimatedDuration { get; private set; }
    public bool IsActive { get; private set; }

    private ServiceCatalogItem()
    {
        Code = string.Empty;
        Name = string.Empty;
        Description = string.Empty;
    }

    public ServiceCatalogItem(Guid tenantId, string code, string name, string description, decimal basePrice, TimeSpan estimatedDuration)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId == Guid.Empty ? throw new ArgumentException("Tenant ID is required.", nameof(tenantId)) : tenantId;
        Code = string.IsNullOrWhiteSpace(code) ? throw new ArgumentException("Item code is required.", nameof(code)) : code;
        Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Item name is required.", nameof(name)) : name;
        Description = description ?? string.Empty;
        BasePrice = basePrice < 0 ? throw new ArgumentOutOfRangeException(nameof(basePrice), "Price cannot be negative.") : basePrice;
        EstimatedDuration = estimatedDuration;
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
