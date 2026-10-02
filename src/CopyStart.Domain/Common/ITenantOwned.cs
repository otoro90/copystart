namespace CopyStart.Domain.Common;

public interface ITenantOwned
{
    Guid TenantId { get; }
}