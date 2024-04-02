using System.Diagnostics.CodeAnalysis;

namespace ItemCatalogue.Core.Models;

public record CategoryId(Guid Value);

public class Category
{
    public required CategoryId Id { get; set; }
    public required string Name { get; set; }
}


