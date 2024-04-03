
using ItemCatalogue.Spa.Angular.Server.Clients;
using ItemCatalogue.Spa.Angular.Server.Options;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;

namespace ItemCatalogue.Spa.Angular.Server.Endpoints.CatalogueEndpoints.Queries;

public static class ItemsList
{
    public static readonly int MAX_ITEMS = 1000;

    public static RouteGroupBuilder MapItemsList(this RouteGroupBuilder group)
    {
        group.MapGet("/{catalogueId}/items", HandleAsync)
                .WithName("GetCatalogueItemsList")
                .WithOpenApi(o =>
                {
                    o.Summary = "Returns Items in the given Catalogue";
                    o.Description = "Returns Items in the given Catalogue limited to 1000 items";

                    return o;
                });

        return group;
    }


    public static async Task<Results<Ok<CatalogueItems>, NotFound>> HandleAsync(
        IItemCatalogueApiClient client,
        Guid catalogueId,
        CancellationToken ct)
    {
        var response = await client.GetCatalogueItemsAsync(catalogueId, ct);

        return response is not null
            ? TypedResults.Ok(response)
            : TypedResults.NotFound();
    }

}
