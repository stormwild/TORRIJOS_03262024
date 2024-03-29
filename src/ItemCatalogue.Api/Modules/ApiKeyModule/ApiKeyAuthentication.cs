using System.Security.Claims;
using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace ItemCatalogue.Api.Modules.ApiKeyModule;

public class ApiKeyAuthentication(ILoggerFactory loggerFactory, IOptionsMonitor<AuthenticationSchemeOptions> options, UrlEncoder encoder, IOptions<AuthenticationSettings> authenticationSettings) : AuthenticationHandler<AuthenticationSchemeOptions>(options, loggerFactory, encoder)
{
    private readonly IOptions<AuthenticationSettings> _authenticationSettings = authenticationSettings;

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (Context.GetEndpoint()?.Metadata.GetMetadata<RequireApiKeyAttribute>() is not null)
        {
            Request.Headers.TryGetValue(ApiKeyConstants.ApiKeyHeaderName, out var apiKey);

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return Task.FromResult(AuthenticateResult.Fail(ApiKeyConstants.ApiKeyNotFound));
            }

            if (apiKey != _authenticationSettings.Value.ApiKey)
            {
                return Task.FromResult(AuthenticateResult.Fail(ApiKeyConstants.ApiKeyInvalid));
            }

            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(), ApiKeyConstants.ApiKeySecurity)));
        }

        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(), ApiKeyConstants.ApiKeySecurity)));
    }
}
