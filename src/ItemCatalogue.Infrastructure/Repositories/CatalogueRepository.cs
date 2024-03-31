using ItemCatalogue.Core.Models;
using ItemCatalogue.Core.Repositories;

using Microsoft.EntityFrameworkCore;

namespace ItemCatalogue.Infrastructure;

public class CatalogueRepository : ICatalogueRepository
{
    readonly CatalogueDbContext _db;

    public CatalogueRepository(CatalogueDbContext db)
    {
        _db = db;
    }

    public async Task<Catalogue?> GetCatalogueItemsAsync(Guid catalogueId, CancellationToken ct)
    {
        return await _db.Catalogues
            .Include(c => c.Items)
            .ThenInclude(i => i.PrimaryCategory)
            .FirstOrDefaultAsync(c => c.Id == new CatalogueId(catalogueId), ct);
    }
}
