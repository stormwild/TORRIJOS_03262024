using ItemCatalogue.Core.Models;

namespace ItemCatalogue.Core.Repositories;

public interface IItemRepository
{
    Task<Catalogue?> GetItemsListAsync(CatalogueId catalogueId, CancellationToken ct);
    Task<Item?> GetItemByIdAsync(CatalogueId catalogueId, ItemId itemId, CancellationToken ct);
    Task<(Item? item, int entries)> CreateItemAsync(Item item, CancellationToken ct);
    Task<(Item? item, int entries)> SaveItemAsync(Item item, CancellationToken ct);
    Task<int> CommitAsync(CancellationToken ct);
}
