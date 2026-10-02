using CopyStart.Domain.Entities;
using CopyStart.Domain.Common;
using CopyStart.Application.Common;
using CopyStart.Domain.Modules.Assets;
using CopyStart.Domain.Modules.Parts;
using CopyStart.Domain.Modules.Procedures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CopyStart.Infrastructure.Persistence;

/// <summary>
/// Target database context for CopyStart PostgreSQL persistence.
/// </summary>
public class CopyStartDbContext : DbContext
{
    private readonly ITenantContext _tenantContext;

    public CopyStartDbContext(DbContextOptions<CopyStartDbContext> options, ITenantContext tenantContext) : base(options)
    {
        _tenantContext = tenantContext;
    }

    public Guid TenantId => _tenantContext.GetTenantId();

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<WorkRequest> WorkRequests => Set<WorkRequest>();
    public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();
    public DbSet<TimelineEvent> TimelineEvents => Set<TimelineEvent>();
    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<Part> Parts => Set<Part>();
    public DbSet<Procedure> Procedures => Set<Procedure>();
    public DbSet<ServiceCatalogItem> ServiceCatalogItems => Set<ServiceCatalogItem>();
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<TenantDomain> TenantDomains => Set<TenantDomain>();
    public DbSet<TenantMembership> TenantMemberships => Set<TenantMembership>();

    public async Task<IDbContextTransaction> BeginTenantTransactionAsync(CancellationToken cancellationToken = default)
    {
        var tenantId = TenantId;
        var transaction = await Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await Database.ExecuteSqlInterpolatedAsync(
                $"SELECT set_config('app.tenant_id', {tenantId.ToString()}, true)",
                cancellationToken);
            return transaction;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            await transaction.DisposeAsync();
            throw;
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureCustomer(modelBuilder.Entity<Customer>());
        ConfigureWorkRequest(modelBuilder.Entity<WorkRequest>());
        ConfigureWorkOrder(modelBuilder.Entity<WorkOrder>());
        ConfigureTimelineEvent(modelBuilder.Entity<TimelineEvent>());
        ConfigureAsset(modelBuilder.Entity<Asset>());
        ConfigurePart(modelBuilder.Entity<Part>());
        ConfigureProcedure(modelBuilder.Entity<Procedure>());
        ConfigureTenant(modelBuilder.Entity<Tenant>());
        ConfigureTenantDomain(modelBuilder.Entity<TenantDomain>());
        ConfigureTenantMembership(modelBuilder.Entity<TenantMembership>());
        ConfigureServiceCatalogItem(modelBuilder.Entity<ServiceCatalogItem>());

        ApplyTenantFilter<Customer>(modelBuilder);
        ApplyTenantFilter<WorkRequest>(modelBuilder);
        ApplyTenantFilter<WorkOrder>(modelBuilder);
        ApplyTenantFilter<TimelineEvent>(modelBuilder);
        ApplyTenantFilter<Asset>(modelBuilder);
        ApplyTenantFilter<Part>(modelBuilder);
        ApplyTenantFilter<Procedure>(modelBuilder);
        ApplyTenantFilter<ServiceCatalogItem>(modelBuilder);
        ApplyTenantFilter<TenantDomain>(modelBuilder);
        ApplyTenantFilter<TenantMembership>(modelBuilder);
    }

    private void ApplyTenantFilter<TEntity>(ModelBuilder modelBuilder) where TEntity : class, ITenantOwned
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(entity => entity.TenantId == TenantId);
    }

    private static void ConfigureCustomer(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Customer> builder)
    {
        builder.HasAlternateKey(customer => new { customer.TenantId, customer.Id });
        builder.Property(customer => customer.TenantId).IsRequired();
        builder.HasIndex(customer => new { customer.TenantId, customer.IdentificationNumber }).IsUnique();
    }

    private static void ConfigureWorkRequest(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WorkRequest> builder)
    {
        builder.HasAlternateKey(request => new { request.TenantId, request.Id });
        builder.Property(request => request.TenantId).IsRequired();
        builder.HasIndex(request => new { request.TenantId, request.TrackingNumber }).IsUnique();
        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(request => new { request.TenantId, request.CustomerId })
            .HasPrincipalKey(customer => new { customer.TenantId, customer.Id })
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Asset>()
            .WithMany()
            .HasForeignKey(request => new { request.TenantId, request.AssetId })
            .HasPrincipalKey(asset => new { asset.TenantId, asset.Id })
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(request => request.Timeline)
            .WithOne()
            .HasForeignKey(eventEntry => new { eventEntry.TenantId, eventEntry.WorkRequestId })
            .HasPrincipalKey(request => new { request.TenantId, request.Id })
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigureWorkOrder(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WorkOrder> builder)
    {
        builder.HasAlternateKey(order => new { order.TenantId, order.Id });
        builder.Property(order => order.TenantId).IsRequired();
        builder.HasIndex(order => new { order.TenantId, order.OrderNumber }).IsUnique();
        builder.HasOne<WorkRequest>()
            .WithMany()
            .HasForeignKey(order => new { order.TenantId, order.WorkRequestId })
            .HasPrincipalKey(request => new { request.TenantId, request.Id })
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(order => order.Timeline)
            .WithOne()
            .HasForeignKey(eventEntry => new { eventEntry.TenantId, eventEntry.WorkOrderId })
            .HasPrincipalKey(order => new { order.TenantId, order.Id })
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigureTimelineEvent(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TimelineEvent> builder)
    {
        builder.HasAlternateKey(eventEntry => new { eventEntry.TenantId, eventEntry.Id });
        builder.Property(eventEntry => eventEntry.TenantId).IsRequired();
    }

    private static void ConfigureAsset(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Asset> builder)
    {
        builder.HasAlternateKey(asset => new { asset.TenantId, asset.Id });
        builder.Property(asset => asset.TenantId).IsRequired();
        builder.HasIndex(asset => new { asset.TenantId, asset.SerialNumber }).IsUnique();
        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(asset => new { asset.TenantId, asset.CustomerId })
            .HasPrincipalKey(customer => new { customer.TenantId, customer.Id })
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigurePart(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Part> builder)
    {
        builder.HasAlternateKey(part => new { part.TenantId, part.Id });
        builder.Property(part => part.TenantId).IsRequired();
        builder.HasIndex(part => new { part.TenantId, part.Sku }).IsUnique();
    }

    private static void ConfigureProcedure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Procedure> builder)
    {
        builder.HasAlternateKey(procedure => new { procedure.TenantId, procedure.Id });
        builder.Property(procedure => procedure.TenantId).IsRequired();
        builder.HasIndex(procedure => new { procedure.TenantId, procedure.Code }).IsUnique();
    }

    private static void ConfigureServiceCatalogItem(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ServiceCatalogItem> builder)
    {
        builder.HasAlternateKey(item => new { item.TenantId, item.Id });
        builder.Property(item => item.TenantId).IsRequired();
        builder.HasIndex(item => new { item.TenantId, item.Code }).IsUnique();
    }

    private static void ConfigureTenant(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tenant> builder)
    {
        builder.HasKey(tenant => tenant.Id);
        builder.Property(tenant => tenant.Name).IsRequired();
        builder.HasIndex(tenant => tenant.Slug).IsUnique();
    }

    private static void ConfigureTenantDomain(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TenantDomain> builder)
    {
        builder.HasAlternateKey(domain => new { domain.TenantId, domain.Id });
        builder.Property(domain => domain.TenantId).IsRequired();
        builder.HasIndex(domain => domain.Hostname).IsUnique();
    }

    private static void ConfigureTenantMembership(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TenantMembership> builder)
    {
        builder.HasAlternateKey(membership => new { membership.TenantId, membership.Id });
        builder.Property(membership => membership.TenantId).IsRequired();
        builder.HasIndex(membership => new { membership.TenantId, membership.UserId }).IsUnique();
    }
}
