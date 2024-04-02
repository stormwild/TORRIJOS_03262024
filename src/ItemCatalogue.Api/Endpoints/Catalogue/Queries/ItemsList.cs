using ItemCatalogue.Api.Endpoints.Catalogue.Queries;
using ItemCatalogue.Core.Repositories;

using Microsoft.AspNetCore.Http.HttpResults;

namespace ItemCatalogue.Api;

public static class ItemsList
{
    public static readonly int MAX_ITEMS = 1000;

    public static RouteGroupBuilder MapItemsList(this RouteGroupBuilder group)
    {
        group.MapGet("/{catalogueId}/items", HandleAsync)
                .WithOpenApi(o =>
                {
                    o.Summary = "Returns Items in the given Catalogue";
                    o.Description = "Returns Items in the given Catalogue limited to 1000 items";

                    return o;
                });

        return group;
    }


    private static async Task<Results<Ok<CatalogueItems>, NotFound>> HandleAsync(
        ICatalogueRepository repository, Guid catalogueId, CancellationToken ct)
    {
        var catalogue = await repository.GetItemsListAsync(catalogueId, ct);

        return catalogue is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(new CatalogueItems(
                catalogue.Id.Value,
                catalogue.Name,
                catalogue.Items.Select(i => new Item(
                    i.Id.Value,
                    i.Name,
                    i.PrimaryCategory.Name))
                    .Take(MAX_ITEMS)
                    .ToList().AsReadOnly()));
    }
}
