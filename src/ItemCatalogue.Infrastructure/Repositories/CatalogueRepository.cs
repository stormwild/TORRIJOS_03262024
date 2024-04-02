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

    public async Task<Catalogue?> GetItemsListAsync(Guid catalogueId, CancellationToken ct)
    {
        return await _db.Catalogues
            .Include(c => c.Items)
            .ThenInclude(i => i.PrimaryCategory)
            .FirstOrDefaultAsync(c => c.Id == new CatalogueId(catalogueId), ct);
    }

    public async Task<Item?> GetItemByIdAsync(Guid catalogueId, Guid itemId, CancellationToken ct)
    {
        return await _db.Items
            .Include(i => i.PrimaryCategory)
            .FirstOrDefaultAsync(i => i.Id == new ItemId(itemId) && i.CatalogueId == new CatalogueId(catalogueId), ct);
    }
}
