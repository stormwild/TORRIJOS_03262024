
using ItemCatalogue.Api.Endpoints.Catalogue.Queries;
using ItemCatalogue.Core.Repositories;

using Microsoft.AspNetCore.Http.HttpResults;

namespace ItemCatalogue.Api;

public static class ItemGetById
{
    public static RouteGroupBuilder MapItemGetById(this RouteGroupBuilder group)
    {
        group.MapGet("/{catalogueId}/items/{itemId}", HandleAsync)
        .WithOpenApi(o =>
        {
            o.Summary = "Returns Item in the given Catalogue";
            o.Description = "Returns Item in the given Catalogue by Catalogue and Item's Id";

            return o;
        });

        return group;
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
