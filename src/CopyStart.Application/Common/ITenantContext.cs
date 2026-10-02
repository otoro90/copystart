namespace CopyStart.Application.Common;

public interface ITenantContext
{
    Guid GetTenantId();
}