using Microsoft.AspNetCore.Authentication;

namespace ItemCatalogue.Api.Modules.AuthenticationModule;

public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
{
    public string ApiKey { get; set; } = "default-api-key";
}
