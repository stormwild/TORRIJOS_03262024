using ItemCatalogue.Api.Endpoints.CatalogueEndpoints.Dtos;
using ItemCatalogue.Core.Models;
using ItemCatalogue.Core.Repositories;

using Microsoft.AspNetCore.Http.HttpResults;

namespace ItemCatalogue.Api.Endpoints.CatalogueEndpoints.Queries;

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

    public static async Task<Results<Ok<CatalogueItem>, NotFound>> HandleAsync(IItemRepository repository, Guid catalogueId, Guid itemId, CancellationToken ct)
    {
        var item = await repository.GetItemByIdAsync(new CatalogueId(catalogueId), new ItemId(itemId), ct);

        return item is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(new CatalogueItem(
                item.Id.Value,
                item.Name,
                item.PrimaryCategory.Name));
    }
}
