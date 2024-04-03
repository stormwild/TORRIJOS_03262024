using ItemCatalogue.Spa.Angular.Server.Clients;
using ItemCatalogue.Spa.Angular.Server.Options;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;

namespace ItemCatalogue.Spa.Angular.Server.Endpoints.CatalogueEndpoints.Queries;

public static class ItemGetById
{
    public static RouteGroupBuilder MapItemGetById(this RouteGroupBuilder group)
    {
        group.MapGet("/{catalogueId}/items/{itemId}", HandleAsync)
             .WithName("GetItemById")
             .WithOpenApi(o =>
             {
                 o.Summary = "Returns Item in the given Catalogue";
                 o.Description = "Returns Item in the given Catalogue by Catalogue and Item's Id";

                 return o;
             });

        return group;
    }

    public static async Task<Results<Ok<CatalogueItem>, NotFound>> HandleAsync(
        IItemCatalogueApiClient client,
        Guid catalogueId, Guid itemId,
        CancellationToken ct)
    {
        var response = await client.GetItemByIdAsync(catalogueId, itemId, ct);

        return response is not null
            ? TypedResults.Ok(response)
            : TypedResults.NotFound();
    }
}
