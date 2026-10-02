using System.Security.Claims;
using CopyStart.Api.Authentication;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace CopyStart.UnitTests;

public class ClaimsTenantContextTests
{
    [Fact]
    public void GetTenantId_ReturnsGuid_WhenTenantIdClaimIsPresent()
    {
        var tenantId = Guid.NewGuid();
        var context = CreateContextWithClaims(new Claim("tenant_id", tenantId.ToString()));

        Assert.Equal(tenantId, context.GetTenantId());
    }

    [Fact]
    public void GetTenantId_ReturnsGuid_WhenZitadelOrgIdClaimIsPresent()
    {
        var tenantId = Guid.NewGuid();
        var context = CreateContextWithClaims(new Claim("org_id", tenantId.ToString()));

        Assert.Equal(tenantId, context.GetTenantId());
    }

    [Fact]
    public void GetTenantId_ReturnsGuid_WhenZitadelUrnOrgIdClaimIsPresent()
    {
        var tenantId = Guid.NewGuid();
        var context = CreateContextWithClaims(new Claim("urn:zitadel:iam:org:id", tenantId.ToString()));

        Assert.Equal(tenantId, context.GetTenantId());
    }

    [Fact]
    public void GetTenantId_ThrowsUnauthorized_WhenClaimIsMissing()
    {
        var context = CreateContextWithClaims();

        Assert.Throws<UnauthorizedAccessException>(() => context.GetTenantId());
    }

    [Fact]
    public void GetTenantId_ThrowsUnauthorized_WhenClaimIsInvalidGuid()
    {
        var context = CreateContextWithClaims(new Claim("tenant_id", "not-a-guid"));

        Assert.Throws<UnauthorizedAccessException>(() => context.GetTenantId());
    }

    [Fact]
    public void GetTenantId_ThrowsUnauthorized_WhenClaimIsEmptyGuid()
    {
        var context = CreateContextWithClaims(new Claim("tenant_id", Guid.Empty.ToString()));

        Assert.Throws<UnauthorizedAccessException>(() => context.GetTenantId());
    }

    [Fact]
    public void GetTenantId_ThrowsUnauthorized_WhenHttpContextIsNull()
    {
        var accessor = new HttpContextAccessor { HttpContext = null };
        var context = new ClaimsTenantContext(accessor);

        Assert.Throws<UnauthorizedAccessException>(() => context.GetTenantId());
    }

    private static ClaimsTenantContext CreateContextWithClaims(params Claim[] claims)
    {
        var identity = new ClaimsIdentity(claims, "TestAuth");
        var principal = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext { User = principal };
        var accessor = new HttpContextAccessor { HttpContext = httpContext };

        return new ClaimsTenantContext(accessor);
    }
}
