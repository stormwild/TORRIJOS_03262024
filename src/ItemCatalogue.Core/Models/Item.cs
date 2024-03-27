namespace ItemCatalogue.Core.Models;

public record ItemId(Guid Value);

public class Item
{
    public required ItemId Id { get; set; }
    public required CatalogueId CatalogueId { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = "";
    public required CategoryId PrimaryCategoryId { get; set; }
    public required Category PrimaryCategory { get; set; }
    public List<Category> Categories { get; set; } = [];
}



