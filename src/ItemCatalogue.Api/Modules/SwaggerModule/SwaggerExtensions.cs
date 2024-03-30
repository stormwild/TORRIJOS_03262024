using ItemCatalogue.Api.Modules.AuthenticationModule;

using NSwag;
using NSwag.Generation.Processors.Security;

namespace ItemCatalogue.Api.Modules.SwaggerModule;

public static class SwaggerExtensions
{
    public static string SwaggerSectionName = "Swagger:Info";
    public static void AddSwaggerConfiguration(this IServiceCollection services, ConfigurationManager config)
    {
        var info = config.GetSection(SwaggerSectionName).Get<OpenApiInfo>();

        ArgumentNullException.ThrowIfNull(info, nameof(info));

        services.AddOpenApiDocument(document =>
        {
            document.DocumentName = info.Title;
            document.Title = info.Title;
            document.Description = info.Description;
            document.Version = info.Version;
            document.PostProcess = d => d.Info = info;

            document.AddSecurity(ApiKeyConstants.ApiKeySecurity, [], new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = ApiKeyConstants.ApiKeyHeaderName,
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = ApiKeyConstants.ApiKeySecurityDescription
            });

            document.OperationProcessors.Add(
                new AspNetCoreOperationSecurityScopeProcessor(ApiKeyConstants.ApiKeySecurity));
        });
    }
}
