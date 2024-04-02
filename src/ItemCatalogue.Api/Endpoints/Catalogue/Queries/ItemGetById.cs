
using ItemCatalogue.Api.Endpoints.Catalogue.Queries;
using ItemCatalogue.Core.Repositories;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Models;

namespace ItemCatalogue.Api;

public static class ItemGetById
{
    public static void MapItemGetById(this WebApplication app)
    {
        app.MapGet("/catalogue/{catalogueId}/items/{itemId}", HandleAsync)
        .WithOpenApi(o =>
        {
            o.Tags = [new OpenApiTag { Name = "Catalog Item" }];
            o.Summary = "Returns Item in the given Catalogue";
            o.Description = "Returns Item in the given Catalogue by Catalogue and Item's Id";

            return o;
        })
        .RequireAuthorization();
    }

    private static async Task<Results<Ok<Item>, NotFound>> HandleAsync(ICatalogueRepository repository, Guid catalogueId, Guid itemId, CancellationToken ct)
    {
        var item = await repository.GetItemByIdAsync(catalogueId, itemId, ct);

        return item is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(new Item(
                item.Id.Value,
                item.Name,
                item.PrimaryCategory.Name));
    }
}
