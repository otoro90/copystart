using CopyStart.Domain.Common;

namespace CopyStart.Domain.Entities;

public class Tenant : IAggregateRoot
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Slug { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Tenant(Guid id, string name, string slug, bool isActive = true)
    {
        Id = id == Guid.Empty ? throw new ArgumentException("Tenant ID cannot be empty.", nameof(id)) : id;
        Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Name cannot be empty.", nameof(name)) : name;
        Slug = string.IsNullOrWhiteSpace(slug) ? throw new ArgumentException("Slug cannot be empty.", nameof(slug)) : slug.ToLowerInvariant();
        IsActive = isActive;
        CreatedAt = DateTime.UtcNow;
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}
