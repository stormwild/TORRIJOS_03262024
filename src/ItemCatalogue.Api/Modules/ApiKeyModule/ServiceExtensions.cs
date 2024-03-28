namespace ItemCatalogue.Api.Modules.ApiKeyModule;

public static class ServiceExtensions
{
    public static void AddApiKeyAuthorization(this IServiceCollection services)
    {
        services.AddTransient<ApiKeyAuthorization>();
    }

    public static void AddApiKeyAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthenticationSettings>(
            configuration.GetSection(AuthenticationSettings.Authentication));
    }
}
