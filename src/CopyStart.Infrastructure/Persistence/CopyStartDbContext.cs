using Microsoft.EntityFrameworkCore;

namespace CopyStart.Infrastructure.Persistence;

/// <summary>
/// Target database context for CopyStart PostgreSQL persistence.
/// NOTE: Entity mappings and migrations remain intentionally frozen until tenant ownership
/// and multi-tenancy are accepted in change 'add-multitenancy-and-zitadel-identity'.
/// </summary>
public class CopyStartDbContext : DbContext
{
    public CopyStartDbContext(DbContextOptions<CopyStartDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Persistence mappings are frozen pending tenant ownership design.
    }
}
