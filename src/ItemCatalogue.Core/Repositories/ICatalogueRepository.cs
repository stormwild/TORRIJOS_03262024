using ItemCatalogue.Core.Models;

namespace ItemCatalogue.Core.Repositories;

public interface ICatalogueRepository
{
    Task<Catalogue> GetCatalogueItemsAsync(Guid catalogueId, CancellationToken ct);
}
