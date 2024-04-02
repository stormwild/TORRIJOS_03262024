using FluentValidation;

using ItemCatalogue.Core.Models;

namespace ItemCatalogue.Api.Endpoints.CatalogueEndpoints.Dtos;

public record SaveItem(string Name, string? Description);

public class SaveItemValidator : AbstractValidator<SaveItem>
{
    public SaveItemValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
    }
}

public static class SaveItemExtensions
{
    public static void MapToNewValues(this Item trackedItem, SaveItem saveItem)
    {
        ArgumentNullException.ThrowIfNull(trackedItem);
        trackedItem.Name = saveItem.Name;
        trackedItem.Description = saveItem.Description ?? string.Empty;
    }
}