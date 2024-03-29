using ItemCatalogue.Core.Models;

namespace ItemCatalogue.Api;

public class PaginatedItemsRequest
{
    public required Guid CatalogueId { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; }
}