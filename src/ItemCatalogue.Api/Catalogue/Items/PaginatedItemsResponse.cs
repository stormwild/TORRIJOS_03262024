using ItemCatalogue.Core.Models;

namespace ItemCatalogue.Api;

public class PaginatedItemsResponse
{
    public int TotalItems { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public IReadOnlyCollection<Item> Items { get; set; } = [];
}

public record Item(ItemId Id, string Name, string Category);

