using ItemCatalogue.Core.Models;

using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace ItemCatalogue.Api.Endpoints.CatalogueEndpoints.Dtos;

public record CreateItem(string Name, string? Description, Guid PrimaryCategoryId, List<Guid> CategoryIds);


public class CreateItemValidator : AbstractValidator<CreateItem>
{
    public CreateItemValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.PrimaryCategoryId).NotEmpty().WithMessage("PrimaryCategoryId is required.");
        RuleFor(x => x.CategoryIds).NotEmpty().WithMessage("At least one CategoryId is required.");
        // Add more rules as needed
    }
}

public static class CreateItemExtensions
{
    public static CategoryId PrimaryCategoryId(this CreateItem createItem) => new(createItem.PrimaryCategoryId);
    public static List<CategoryId> CategoryIds(this CreateItem createItem) => createItem.CategoryIds.Select(c => new CategoryId(c)).ToList();

    public static Item MapToItem(this CreateItem createItem, ItemId itemId, Catalogue catalogue, Category category, List<Category> categories)
    {
        ArgumentNullException.ThrowIfNull(createItem);
        ArgumentNullException.ThrowIfNull(catalogue);
        ArgumentNullException.ThrowIfNull(category);
        ArgumentNullException.ThrowIfNull(categories);

        return new Item
        {
            Id = itemId,
            CatalogueId = catalogue.Id,
            Name = createItem.Name,
            Description = createItem.Description ?? string.Empty,
            PrimaryCategoryId = category.Id,
            PrimaryCategory = category,
            Categories = categories
        };
    }
}