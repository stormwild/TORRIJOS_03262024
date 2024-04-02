using ItemCatalogue.Core.Models;

namespace ItemCatalogue.Infrastructure.Repositories;

public interface ICatalogueRepository
{
    Task<Catalogue?> GetCatalogueAsync(CatalogueId catalogueId, CancellationToken ct);
}