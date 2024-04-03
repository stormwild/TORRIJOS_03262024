using ItemCatalogue.Spa.Angular.Server.Endpoints.CatalogueEndpoints.Queries;

using Microsoft.OpenApi.Models;

namespace ItemCatalogue.Spa.Angular.Server.Endpoints.CatalogueEndpoints;

public static class CatalogueEndpoints
{
    public static void MapCatalogue(this WebApplication app)
    {
        var group = app.MapGroup("/catalogue");

        group.MapItemsList();
        group.MapItemGetById();

        group.WithOpenApi(o =>
        {
            o.Tags = [new OpenApiTag { Name = "Catalogue" }];
            return o;
        });
    }
}
