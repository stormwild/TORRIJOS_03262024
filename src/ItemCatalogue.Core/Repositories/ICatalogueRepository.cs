using ItemCatalogue.Core.Models;

namespace ItemCatalogue.Core.Repositories;

public interface ICatalogueRepository
{
    Task<Catalogue?> GetItemsListAsync(Guid catalogueId, CancellationToken ct);
    Task<Item?> GetItemByIdAsync(Guid catalogueId, Guid itemId, CancellationToken ct);
}
