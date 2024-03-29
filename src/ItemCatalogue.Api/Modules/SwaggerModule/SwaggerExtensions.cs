using FastEndpoints.Swagger;

using NSwag;
using NSwag.Generation.Processors.Security;

namespace ItemCatalogue.Api.Modules.SwaggerModule;

public static class SwaggerExtensions
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddOpenApiDocument(document =>
        {
            document.DocumentName = "Item Catalogue API v1";
            document.Title = "Item Catalogue API";
            document.Description = "A simple API to manage items in a catalogue";
            document.Version = "1.0";
            document.PostProcess = document =>
            {
                document.Info.Title = "Item Catalogue API";
                document.Info.Description = "A simple API to manage items in a catalogue";
            };

            document.AddSecurity("ApiKey", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "X-API-KEY",
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "API key needed to access the endpoints"
            });

            document.OperationProcessors.Add(
                new AspNetCoreOperationSecurityScopeProcessor("ApiKey"));
        });
    }

    public static DocumentOptions SetOptions(this DocumentOptions o, WebApplicationBuilder builder)
    {
        var info = builder.Configuration.GetSection("OpenApiInfo").Get<OpenApiInfo>();

        ArgumentNullException.ThrowIfNull(info, nameof(info));

        o.EnableJWTBearerAuth = false;
        o.DocumentSettings = document =>
            {
                document.DocumentName = info.Title;
                document.Title = info.Title;
                document.Description = info.Description;
                document.Version = info.Version;
                document.PostProcess = (d) =>
                {
                    d.Info = info;
                };

                document.AddSecurity(ApiKeyConstants.ApiKeySecurity, Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = ApiKeyConstants.ApiKeyHeaderName,
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = ApiKeyConstants.ApiKeySecurityDescription
                });

                document.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor(ApiKeyConstants.ApiKeySecurity));
            };

        return o;
    }
}
