using CopyStart.Domain.Entities;
using CopyStart.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CopyStart.UnitTests;

public class TenantDatabaseIsolationTests
{
    [PostgreSqlFact]
    public async Task TransactionLocalTenantContext_DoesNotLeakAcrossPooledConnectionReuse()
    {
        var adminConnectionString = Environment.GetEnvironmentVariable("COPYSTART_TEST_POSTGRES_CONNECTION")!;
        var connectionStringBuilder = new Npgsql.NpgsqlConnectionStringBuilder(adminConnectionString)
        {
            Pooling = true,
            MaxPoolSize = 1
        };
        var options = new DbContextOptionsBuilder<CopyStartDbContext>()
            .UseNpgsql(connectionStringBuilder.ConnectionString)
            .Options;
        var firstTenantId = Guid.NewGuid();
        var secondTenantId = Guid.NewGuid();

        await using (var setupContext = CreateContext(options, Guid.NewGuid()))
        {
            await setupContext.Database.MigrateAsync();
            await setupContext.Database.ExecuteSqlRawAsync("""
                DO $role$
                BEGIN
                    IF NOT EXISTS (SELECT 1 FROM pg_roles WHERE rolname = 'copystart_rls_test') THEN
                        CREATE ROLE copystart_rls_test NOLOGIN NOBYPASSRLS;
                    END IF;
                END
                $role$;
                ALTER ROLE copystart_rls_test NOLOGIN NOBYPASSRLS;
                GRANT USAGE ON SCHEMA public TO copystart_rls_test;
                GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA public TO copystart_rls_test;
                """);
        }

        await using (var firstContext = CreateContext(options, firstTenantId))
        {
            await firstContext.Database.OpenConnectionAsync();
            await using var transaction = await firstContext.BeginTenantTransactionAsync();
            await firstContext.Database.ExecuteSqlRawAsync("SET LOCAL ROLE copystart_rls_test");
            firstContext.Customers.Add(new Customer(firstTenantId, "First tenant", "first@example.test", "555-0101", "FIRST-1", "Address"));
            await firstContext.SaveChangesAsync();
            Assert.True(await firstContext.Customers.IgnoreQueryFilters().AnyAsync(customer => customer.IdentificationNumber == "FIRST-1"));
            await transaction.CommitAsync();

            var setting = await ReadTenantSettingAsync(firstContext);
            Assert.True(string.IsNullOrEmpty(setting));
        }

        await using (var secondContext = CreateContext(options, secondTenantId))
        {
            await using var transaction = await secondContext.BeginTenantTransactionAsync();
            await secondContext.Database.ExecuteSqlRawAsync("SET LOCAL ROLE copystart_rls_test");
            Assert.False(await secondContext.Customers.IgnoreQueryFilters().AnyAsync(customer => customer.IdentificationNumber == "FIRST-1"));
            await transaction.CommitAsync();
        }
    }

    private static CopyStartDbContext CreateContext(DbContextOptions<CopyStartDbContext> options, Guid tenantId)
    {
        return new CopyStartDbContext(options, new FixedTenantContext(tenantId));
    }

    private static async Task<string?> ReadTenantSettingAsync(CopyStartDbContext context)
    {
        await using var command = context.Database.GetDbConnection().CreateCommand();
        command.CommandText = "SELECT current_setting('app.tenant_id', true)";
        return (string?)await command.ExecuteScalarAsync();
    }

    private sealed class FixedTenantContext(Guid tenantId) : CopyStart.Application.Common.ITenantContext
    {
        public Guid GetTenantId() => tenantId;
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class PostgreSqlFactAttribute : FactAttribute
    {
        public PostgreSqlFactAttribute()
        {
            if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("COPYSTART_TEST_POSTGRES_CONNECTION")))
            {
                Skip = "Set COPYSTART_TEST_POSTGRES_CONNECTION to run PostgreSQL isolation tests.";
            }
        }
    }
}