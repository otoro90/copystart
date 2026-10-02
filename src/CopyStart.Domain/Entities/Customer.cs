using CopyStart.Domain.Common;

namespace CopyStart.Domain.Entities;

public class Customer : IAggregateRoot, ITenantOwned
{
    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string IdentificationNumber { get; private set; }
    public string Address { get; private set; }
    public bool IsActive { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    private Customer()
    {
        Name = string.Empty;
        Email = string.Empty;
        Phone = string.Empty;
        IdentificationNumber = string.Empty;
        Address = string.Empty;
    }

    public Customer(Guid tenantId, string name, string email, string phone, string identificationNumber, string address)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId == Guid.Empty ? throw new ArgumentException("Tenant ID is required.", nameof(tenantId)) : tenantId;
        Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Customer name is required.", nameof(name)) : name;
        Email = string.IsNullOrWhiteSpace(email) ? throw new ArgumentException("Customer email is required.", nameof(email)) : email;
        Phone = phone ?? string.Empty;
        IdentificationNumber = identificationNumber ?? string.Empty;
        Address = address ?? string.Empty;
        IsActive = true;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void UpdateContactInfo(string email, string phone, string address)
    {
        Email = string.IsNullOrWhiteSpace(email) ? throw new ArgumentException("Customer email is required.", nameof(email)) : email;
        Phone = phone ?? string.Empty;
        Address = address ?? string.Empty;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
