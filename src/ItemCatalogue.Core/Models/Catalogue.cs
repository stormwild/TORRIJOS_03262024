using System.ComponentModel.DataAnnotations;

namespace ItemCatalogue.Core.Models;

public record CatalogueId(Guid Value);

public class Catalogue
{
    public required CatalogueId Id { get; set; }
    public required string Name { get; set; }
    public List<Item> Items { get; set; } = [];
}



