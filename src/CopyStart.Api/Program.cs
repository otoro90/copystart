using CopyStart.Api.Authentication;
using CopyStart.Api.HealthChecks;
using CopyStart.Api.Middleware;
using CopyStart.Application;
using CopyStart.Infrastructure;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Wolverine;
using Wolverine.FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Wolverine configuration with FluentValidation middleware (no transactional outbox)
builder.Host.UseWolverine(opts =>
{
    opts.Discovery.IncludeAssembly(typeof(IApplicationAssemblyMarker).Assembly);
    opts.UseFluentValidation();
});

// Add MVC Controllers and Problem Details
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
builder.Services.AddProblemDetails();

// Built-in OpenAPI
builder.Services.AddOpenApi();

// Register FluentValidation validators
builder.Services.AddValidatorsFromAssemblyContaining<IApplicationAssemblyMarker>();

// Infrastructure layer
builder.Services.AddInfrastructure(builder.Configuration);

// Authentication & Authorization: fail-closed until ZITADEL
// In Testing environment, allows X-Test-Actor: staff
builder.Services.AddAuthentication(TestAuthenticationHandler.SchemeName)
    .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(TestAuthenticationHandler.SchemeName, _ => { });

builder.Services.AddAuthorization();

// Health checks: separate liveness (200 OK) and readiness (503 while persistence schema is frozen)
builder.Services.AddHealthChecks()
    .AddCheck<ProcessLivenessHealthCheck>("live", tags: ["live"])
    .AddCheck<FrozenPersistenceReadinessHealthCheck>("ready", tags: ["ready"]);

var app = builder.Build();

// Custom Problem Details exception handling
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapOpenApi();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Health check endpoints
app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("live")
});
app.MapHealthChecks("/healthz", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("live")
});
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});
app.MapHealthChecks("/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

app.Run();

public partial class Program { }
