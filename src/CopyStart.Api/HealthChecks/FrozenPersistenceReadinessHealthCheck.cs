using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CopyStart.Api.HealthChecks;

public class FrozenPersistenceReadinessHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(HealthCheckResult.Unhealthy(
            "Persistence is unavailable until tenant-scoped PostgreSQL repositories and ZITADEL identity resolution are operational."));
    }
}

public class ProcessLivenessHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(HealthCheckResult.Healthy("API host process is healthy and responsive."));
    }
}
