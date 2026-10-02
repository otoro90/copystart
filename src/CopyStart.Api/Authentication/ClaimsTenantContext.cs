using System.Security.Claims;
using CopyStart.Application.Common;

namespace CopyStart.Api.Authentication;

public sealed class ClaimsTenantContext(IHttpContextAccessor httpContextAccessor) : ITenantContext
{
    public Guid GetTenantId()
    {
        var httpContext = httpContextAccessor.HttpContext;
        var tenantClaim = httpContext?.User.FindFirstValue("tenant_id")
            ?? httpContext?.User.FindFirstValue("org_id")
            ?? httpContext?.User.FindFirstValue("urn:zitadel:iam:org:id");

        if (!Guid.TryParse(tenantClaim, out var tenantId) || tenantId == Guid.Empty)
        {
            throw new UnauthorizedAccessException("An authenticated tenant context is required.");
        }

        return tenantId;
    }
}