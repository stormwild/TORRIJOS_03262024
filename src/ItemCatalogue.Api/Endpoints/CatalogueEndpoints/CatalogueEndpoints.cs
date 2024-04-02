using ItemCatalogue.Api.Endpoints.CatalogueEndpoints.Commands;
using ItemCatalogue.Api.Endpoints.CatalogueEndpoints.Queries;

using Microsoft.OpenApi.Models;

namespace ItemCatalogue.Api.Endpoints.CatalogueEndpoints;

public static class CatalogueEndpoints
{
    public static void MapCatalogue(this WebApplication app)
    {
        var group = app.MapGroup("/catalogue");

        group.MapItemsList();
        group.MapItemGetById();

        group.MapCreateItem();

        group.WithOpenApi(o =>
        {
            o.Tags = [new OpenApiTag { Name = "Catalogue" }];
            return o;
        })
        .RequireAuthorization();
    }
}
