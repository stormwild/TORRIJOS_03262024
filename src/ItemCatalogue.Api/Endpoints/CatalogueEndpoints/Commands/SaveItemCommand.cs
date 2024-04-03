using FluentValidation;

using ItemCatalogue.Api.Endpoints.CatalogueEndpoints.Dtos;
using ItemCatalogue.Core.Models;
using ItemCatalogue.Core.Repositories;
using ItemCatalogue.Infrastructure.Repositories;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ItemCatalogue.Api.Endpoints.CatalogueEndpoints.Commands;

public static class SaveItemCommand
{
    public static RouteGroupBuilder MapSaveItem(this RouteGroupBuilder group)
    {
        group.MapPut("/{catalogueId}/items/{itemId}", HandleAsync)
             .WithName("UpdateItem")
             .WithOpenApi(o =>
             {
                 o.Summary = "Updates the given Item in the given Catalogue";
                 o.Description = "Returns the newly created Items in the given Catalogue with the new values";

                 return o;
             });

        return group;
    }

    public static async Task<Results<Ok<Item>, ValidationProblem, BadRequest>> HandleAsync(
        IValidator<SaveItem> validator,
        ICatalogueRepository catalogueRepository,
        ICategoryRepository categoryRepository,
        IItemRepository itemRepository,
        Guid catalogueId,
        Guid itemId,
        [FromBody] SaveItem saveItem,
        HttpContext ctx,
        CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(saveItem, ct);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(
                validationResult.Errors.ToDictionary(
                    kvp => kvp.PropertyName,
                    kvp => validationResult.Errors
                                           .Where(e => e.PropertyName == kvp.PropertyName)
                                           .Select(e => e.ErrorMessage).ToArray()));
        }

        var item = await itemRepository.GetItemByIdAsync(new CatalogueId(catalogueId), new ItemId(itemId), ct);

        if (item is null)
        {
            return TypedResults.BadRequest();
        }

        var catalogue = await catalogueRepository.GetCatalogueAsync(new CatalogueId(catalogueId), ct);
        if (catalogue is null)
        {
            return TypedResults.BadRequest();
        }

        item.MapToNewValues(saveItem);
        var entries = await itemRepository.CommitAsync(ct);

        return entries > 0
               ? TypedResults.Ok(item)
               : TypedResults.BadRequest();
    }

}
