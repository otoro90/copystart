using CopyStart.Domain.Entities;
using CopyStart.Domain.Modules.Assets;
using CopyStart.Domain.Modules.Parts;
using CopyStart.Domain.Modules.Procedures;
using CopyStart.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;
using Xunit;

namespace CopyStart.UnitTests;

public class TenantOwnershipTests
{
    [Fact]
    public void TenantOwnedAggregates_RequireTenantAndPropagateItToAuditEvents()
    {
        var tenantId = Guid.NewGuid();
        var customer = new Customer(tenantId, "Acme Corp", "contact@acme.com", "555-1234", "TAX-900123", "Main St");
        var request = new WorkRequest(tenantId, customer.Id, "Repair", "Ana", "555-0100");
        var order = new WorkOrder(tenantId, request.Id, "tech-1", actor: "dispatcher");
        var asset = new Asset(tenantId, customer.Id, "SN-1", "Canon", "Model X", "Office printer");
        var part = new Part(tenantId, "PART-1", "Roller", "Feed roller", 10m, 4);
        var procedure = new Procedure(tenantId, "PROC-1", "Replace roller", "Remove the cover.");
        var catalogItem = new ServiceCatalogItem(tenantId, "SERVICE-1", "Repair", "Printer repair", 100m, TimeSpan.FromHours(1));

        Assert.Equal(tenantId, customer.TenantId);
        Assert.Equal(tenantId, request.TenantId);
        Assert.Equal(tenantId, order.TenantId);
        Assert.Equal(tenantId, asset.TenantId);
        Assert.Equal(tenantId, part.TenantId);
        Assert.Equal(tenantId, procedure.TenantId);
        Assert.Equal(tenantId, catalogItem.TenantId);
        Assert.All(request.Timeline, item => Assert.Equal(tenantId, item.TenantId));
        Assert.All(order.Timeline, item => Assert.Equal(tenantId, item.TenantId));
    }

    [Fact]
    public void TenantOwnedAggregate_RejectsEmptyTenantId()
    {
        Assert.Throws<ArgumentException>(() => new Customer(Guid.Empty, "Acme Corp", "contact@acme.com", "555-1234", "TAX-900123", "Main St"));
        Assert.Throws<ArgumentException>(() => new WorkRequest(Guid.Empty, Guid.NewGuid(), "Repair", "Ana", "555-0100"));
        Assert.Throws<ArgumentException>(() => new WorkOrder(Guid.Empty, Guid.NewGuid(), "tech-1"));
        Assert.Throws<ArgumentException>(() => new Asset(Guid.Empty, Guid.NewGuid(), "SN-1", "Canon", "Model X", "Office printer"));
        Assert.Throws<ArgumentException>(() => new Part(Guid.Empty, "PART-1", "Roller", "Feed roller", 10m, 4));
        Assert.Throws<ArgumentException>(() => new Procedure(Guid.Empty, "PROC-1", "Replace roller", "Remove the cover."));
        Assert.Throws<ArgumentException>(() => new ServiceCatalogItem(Guid.Empty, "SERVICE-1", "Repair", "Printer repair", 100m, TimeSpan.FromHours(1)));
    }

    [Fact]
    public void RelationalModel_UsesTenantQualifiedKeysAndRelationships()
    {
        var options = new DbContextOptionsBuilder<CopyStartDbContext>()
            .UseNpgsql("Host=localhost;Database=copystart_test;Username=test;Password=test")
            .Options;
        using var context = new CopyStartDbContext(options, new FixedTenantContext(Guid.NewGuid()));
        var model = context.Model;
        var order = model.FindEntityType(typeof(WorkOrder))!;
        var request = model.FindEntityType(typeof(WorkRequest))!;

        Assert.Contains(order.GetKeys(), key => HasProperties(key.Properties, "TenantId", "Id"));
        Assert.Contains(order.GetIndexes(), index => HasProperties(index.Properties, "TenantId", "OrderNumber") && index.IsUnique);
        Assert.Contains(order.GetForeignKeys(), foreignKey =>
            foreignKey.PrincipalEntityType.ClrType == typeof(WorkRequest)
            && HasProperties(foreignKey.Properties, "TenantId", "WorkRequestId")
            && HasProperties(foreignKey.PrincipalKey.Properties, "TenantId", "Id"));
        Assert.Contains(request.GetIndexes(), index => HasProperties(index.Properties, "TenantId", "TrackingNumber") && index.IsUnique);
    }

    [Fact]
    public void TenantQueries_AreFilteredByTheContextTenant()
    {
        var tenantId = Guid.NewGuid();
        var options = new DbContextOptionsBuilder<CopyStartDbContext>()
            .UseNpgsql("Host=localhost;Database=copystart_test;Username=test;Password=test")
            .Options;
        using var context = new CopyStartDbContext(options, new FixedTenantContext(tenantId));

        var sql = context.WorkOrders.ToQueryString();

        Assert.Contains("TenantId", sql);
        Assert.Contains(tenantId.ToString(), sql);
    }

    [Fact]
    public void MigrationSql_EnablesAndForcesTenantRowLevelSecurity()
    {
        var tenantId = Guid.NewGuid();
        var options = new DbContextOptionsBuilder<CopyStartDbContext>()
            .UseNpgsql("Host=localhost;Database=copystart_test;Username=test;Password=test")
            .Options;
        using var context = new CopyStartDbContext(options, new FixedTenantContext(tenantId));
        var sql = context.GetService<IMigrator>().GenerateScript();

        Assert.Contains("ENABLE ROW LEVEL SECURITY", sql, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("FORCE ROW LEVEL SECURITY", sql, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("CREATE POLICY", sql, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("current_setting", sql, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("app.tenant_id", sql, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void TenantAndMemberships_EnforceInvariantsAndTenantIsolation()
    {
        var tenantId = Guid.NewGuid();
        var tenant = new Tenant(tenantId, "Acme Services", "acme-services");
        var domain = new TenantDomain(tenantId, "acme.copystart.local", isPrimary: true);
        var membership = new TenantMembership(tenantId, "user-123", "Technician");

        Assert.Equal(tenantId, tenant.Id);
        Assert.Equal("acme-services", tenant.Slug);
        Assert.True(tenant.IsActive);

        Assert.Equal(tenantId, domain.TenantId);
        Assert.Equal("acme.copystart.local", domain.Hostname);

        Assert.Equal(tenantId, membership.TenantId);
        Assert.Equal("user-123", membership.UserId);
        Assert.True(membership.IsActive);

        membership.Revoke();
        Assert.False(membership.IsActive);

        tenant.Deactivate();
        Assert.False(tenant.IsActive);

        Assert.Throws<ArgumentException>(() => new Tenant(Guid.Empty, "Name", "slug"));
        Assert.Throws<ArgumentException>(() => new TenantDomain(Guid.Empty, "host.local"));
        Assert.Throws<ArgumentException>(() => new TenantMembership(Guid.Empty, "user", "Role"));
    }

    private static bool HasProperties(IReadOnlyList<IProperty> properties, params string[] names)
    {
        return properties.Select(property => property.Name).SequenceEqual(names);
    }

    private sealed class FixedTenantContext(Guid tenantId) : CopyStart.Application.Common.ITenantContext
    {
        public Guid GetTenantId() => tenantId;
    }
}