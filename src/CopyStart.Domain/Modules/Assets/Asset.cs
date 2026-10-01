using CopyStart.Domain.Common;

namespace CopyStart.Domain.Modules.Assets;

public class Asset : IAggregateRoot
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public string SerialNumber { get; private set; }
    public string Brand { get; private set; }
    public string Model { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }

    private Asset()
    {
        SerialNumber = string.Empty;
        Brand = string.Empty;
        Model = string.Empty;
        Description = string.Empty;
    }

    public Asset(Guid customerId, string serialNumber, string brand, string model, string description)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        SerialNumber = string.IsNullOrWhiteSpace(serialNumber) ? throw new ArgumentException("Serial number is required.", nameof(serialNumber)) : serialNumber;
        Brand = brand ?? string.Empty;
        Model = model ?? string.Empty;
        Description = description ?? string.Empty;
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
