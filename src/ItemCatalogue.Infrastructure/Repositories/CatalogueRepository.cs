using ItemCatalogue.Core.Models;

using Microsoft.EntityFrameworkCore;

namespace ItemCatalogue.Infrastructure.Repositories;

public class CatalogueRepository : ICatalogueRepository
{
    private readonly CatalogueDbContext _db;

    public CatalogueRepository(CatalogueDbContext db)
    {
        _db = db;
    }

    public async Task<Catalogue?> GetCatalogueAsync(CatalogueId catalogueId, CancellationToken ct)
    {
        return await _db.Catalogues
            .Include(c => c.Items)
            .ThenInclude(i => i.PrimaryCategory)
            .FirstOrDefaultAsync(c => c.Id == catalogueId, ct);
    }
}
