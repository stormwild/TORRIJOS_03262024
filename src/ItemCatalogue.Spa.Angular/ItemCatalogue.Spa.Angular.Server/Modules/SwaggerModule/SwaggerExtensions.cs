

using NSwag;

namespace ItemCatalogue.Spa.Angular.Server;

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

        });
    }
}
