using FluentValidation;

using ItemCatalogue.Core.Models;

namespace ItemCatalogue.Api.Endpoints.CatalogueEndpoints.Dtos;

public record SaveItem(string Name, string? Description);

public class SaveItemValidator : AbstractValidator<SaveItem>
{
    public SaveItemValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        // RuleFor(x => x.PrimaryCategoryId).NotEmpty().WithMessage("PrimaryCategoryId is required.");
        // RuleFor(x => x.CategoryIds).NotEmpty().WithMessage("At least one CategoryId is required.");
        // Add more rules as needed
    }
}

public static class SaveItemExtensions
{
    // public static CategoryId PrimaryCategoryId(this SaveItem saveItem) => new(saveItem.PrimaryCategoryId);
    // public static List<CategoryId> CategoryIds(this SaveItem saveItem) => saveItem.CategoryIds.Select(c => new CategoryId(c)).ToList();

    public static void MapToNewValues(this Item trackedItem, SaveItem saveItem)
    {
        // , Catalogue catalogue, Category category, List<Category> categories
        ArgumentNullException.ThrowIfNull(trackedItem);
        // ArgumentNullException.ThrowIfNull(catalogue);
        // ArgumentNullException.ThrowIfNull(category);
        // ArgumentNullException.ThrowIfNull(categories);

        // trackedItem.CatalogueId = catalogue.Id;
        trackedItem.Name = saveItem.Name;
        trackedItem.Description = saveItem.Description ?? string.Empty;
        // trackedItem.PrimaryCategoryId = category.Id;
        // trackedItem.PrimaryCategory = category;
        // trackedItem.Categories = categories;
    }
}