using ItemCatalogue.Api.Endpoints.CatalogueEndpoints.Dtos;
using ItemCatalogue.Core.Models;
using ItemCatalogue.Core.Repositories;
using ItemCatalogue.Infrastructure;

using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ItemCatalogue.Api.Endpoints.CatalogueEndpoints.Commands;

public static class CreateItemCommand
{
    public static RouteGroupBuilder MapCreateItem(this RouteGroupBuilder group)
    {
        group.MapPost("/{catalogueId}/items/{itemId}", HandleAsync)
             .WithOpenApi(o =>
             {
                 o.Summary = "Returns Items in the given Catalogue";
                 o.Description = "Returns Items in the given Catalogue limited to 1000 items";

                 return o;
             });

        return group;
    }

    private static async Task<Results<Created<Item>, BadRequest>> HandleAsync(
        ICatalogueRepository catalogueRepository,
        ICategoryRepository categoryRepository,
        IItemRepository itemRepository,
        Guid catalogueId,
        Guid itemId,
        [FromBody] CreateItem createItem,
        HttpContext ctx,
        CancellationToken ct)
    {
        var catalogue = await catalogueRepository.GetCatalogueAsync(new CatalogueId(catalogueId), ct);
        var category = await categoryRepository.GetCategoryAsync(createItem.PrimaryCategoryId(), ct);
        var categories = await categoryRepository.GetCategoriesByIdsAsync(createItem.CategoryIds(), ct);

        if (catalogue is null || category is null || categories.Count != createItem.CategoryIds.Count)
        {
            return TypedResults.BadRequest();
        }

        var item = createItem.MapToItem(new ItemId(itemId), catalogue, category, categories);
        var (createdItem, entries) = await itemRepository.CreateItemAsync(item, ct);

        return createdItem is not null && entries > 0
               ? TypedResults.Created(ctx.Request.GetDisplayUrl(), createdItem)
               : TypedResults.BadRequest();
    }
}
