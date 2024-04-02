using Microsoft.OpenApi.Models;

namespace ItemCatalogue.Api;

public static class Catalogue
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
        })
        .RequireAuthorization();
    }
}
