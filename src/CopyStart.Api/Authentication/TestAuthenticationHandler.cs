using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CopyStart.Api.Authentication;

public class TestAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public const string SchemeName = "TestAuth";
    public const string ActorHeader = "X-Test-Actor";
    private readonly IHostEnvironment _environment;

    public TestAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IHostEnvironment environment)
        : base(options, logger, encoder)
    {
        _environment = environment;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Only allow test authentication in Testing environment
        if (!_environment.IsEnvironment("Testing"))
        {
            return Task.FromResult(AuthenticateResult.Fail("Test authentication scheme is only active in Testing environment."));
        }

        if (!Request.Headers.TryGetValue(ActorHeader, out var actorValue) || string.IsNullOrWhiteSpace(actorValue))
        {
            return Task.FromResult(AuthenticateResult.Fail($"Missing required '{ActorHeader}' header."));
        }

        var actor = actorValue.ToString();
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, actor),
            new Claim(ClaimTypes.Name, actor),
            new Claim(ClaimTypes.Role, actor),
            new Claim("tenant_id", "11111111-1111-4111-8111-111111111111")
        };

        var identity = new ClaimsIdentity(claims, SchemeName);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, SchemeName);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
