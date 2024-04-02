using ItemCatalogue.Core.Models;

namespace ItemCatalogue.Infrastructure;

public interface ICatalogueRepository
{
    Task<Catalogue?> GetCatalogueAsync(CatalogueId catalogueId, CancellationToken ct);
}