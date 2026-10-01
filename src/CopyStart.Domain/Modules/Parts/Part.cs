using CopyStart.Domain.Common;

namespace CopyStart.Domain.Modules.Parts;

public class Part : IAggregateRoot
{
    public Guid Id { get; private set; }
    public string Sku { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal UnitCost { get; private set; }
    public int AvailableStock { get; private set; }
    public bool IsActive { get; private set; }

    private Part()
    {
        Sku = string.Empty;
        Name = string.Empty;
        Description = string.Empty;
    }

    public Part(string sku, string name, string description, decimal unitCost, int availableStock)
    {
        Id = Guid.NewGuid();
        Sku = string.IsNullOrWhiteSpace(sku) ? throw new ArgumentException("SKU is required.", nameof(sku)) : sku;
        Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Part name is required.", nameof(name)) : name;
        Description = description ?? string.Empty;
        UnitCost = unitCost < 0 ? throw new ArgumentOutOfRangeException(nameof(unitCost), "Unit cost cannot be negative.") : unitCost;
        AvailableStock = availableStock < 0 ? throw new ArgumentOutOfRangeException(nameof(availableStock), "Stock cannot be negative.") : availableStock;
        IsActive = true;
    }

    public void AdjustStock(int quantity)
    {
        if (AvailableStock + quantity < 0)
        {
            throw new InvalidOperationException($"Insufficient stock for part {Sku}. Available: {AvailableStock}, adjustment: {quantity}");
        }
        AvailableStock += quantity;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
