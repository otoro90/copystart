using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CopyStart.Api.HealthChecks;

public class FrozenPersistenceReadinessHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        // Fail readiness closed (HTTP 503) while database schema is frozen pending multi-tenancy and ZITADEL identity design
        return Task.FromResult(HealthCheckResult.Unhealthy(
            "Persistence schema is frozen pending multi-tenancy design acceptance in 'add-multitenancy-and-zitadel-identity'."));
    }
}

public class ProcessLivenessHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(HealthCheckResult.Healthy("API host process is healthy and responsive."));
    }
}
