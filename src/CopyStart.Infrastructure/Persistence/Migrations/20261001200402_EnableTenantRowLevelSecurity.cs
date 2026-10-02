using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CopyStart.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EnableTenantRowLevelSecurity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                DO $tenant_rls$
                DECLARE
                    table_name text;
                BEGIN
                    FOREACH table_name IN ARRAY ARRAY[
                        'Customers',
                        'WorkRequests',
                        'WorkOrders',
                        'TimelineEvents',
                        'Assets',
                        'Parts',
                        'Procedures',
                        'ServiceCatalogItems'
                    ]
                    LOOP
                        EXECUTE format('ALTER TABLE %I ENABLE ROW LEVEL SECURITY', table_name);
                        EXECUTE format('ALTER TABLE %I FORCE ROW LEVEL SECURITY', table_name);
                        EXECUTE format(
                            'CREATE POLICY tenant_isolation ON %I USING ("TenantId" = NULLIF(current_setting(''app.tenant_id'', true), '''')::uuid) WITH CHECK ("TenantId" = NULLIF(current_setting(''app.tenant_id'', true), '''')::uuid)',
                            table_name);
                    END LOOP;
                END
                $tenant_rls$;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                DO $tenant_rls$
                DECLARE
                    table_name text;
                BEGIN
                    FOREACH table_name IN ARRAY ARRAY[
                        'Customers',
                        'WorkRequests',
                        'WorkOrders',
                        'TimelineEvents',
                        'Assets',
                        'Parts',
                        'Procedures',
                        'ServiceCatalogItems'
                    ]
                    LOOP
                        EXECUTE format('DROP POLICY IF EXISTS tenant_isolation ON %I', table_name);
                        EXECUTE format('ALTER TABLE %I NO FORCE ROW LEVEL SECURITY', table_name);
                        EXECUTE format('ALTER TABLE %I DISABLE ROW LEVEL SECURITY', table_name);
                    END LOOP;
                END
                $tenant_rls$;
                """);
        }
    }
}
