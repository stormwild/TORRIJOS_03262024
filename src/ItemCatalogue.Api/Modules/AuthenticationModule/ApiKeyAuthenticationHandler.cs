using System.Security.Claims;
using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace ItemCatalogue.Api.Modules.AuthenticationModule;

public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
{
    public ApiKeyAuthenticationHandler(
        IOptionsMonitor<ApiKeyAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder)
        : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("X-Api-Key", out var apiKey))
        {
            Task.FromResult(AuthenticateResult.Fail("X-API-Key Header not found"));
        }

        if (string.Compare(apiKey, Options.ApiKey, StringComparison.Ordinal) != 0)
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid API Key"));
        }

        var principal = BuildPrincipal(Scheme.Name, Options.ApiKey, "APIKey");

        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name)));
    }

    static ClaimsPrincipal BuildPrincipal(string schemeName, string name, string issuer, params Claim[] claims)
    {
        var identity = new ClaimsIdentity(schemeName);

        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, name, ClaimValueTypes.String, issuer));
        identity.AddClaim(new Claim(ClaimTypes.Name, name, ClaimValueTypes.String, issuer));

        identity.AddClaims(claims);

        var principal = new ClaimsPrincipal(identity);
        return principal;
    }


}