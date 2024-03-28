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
}
