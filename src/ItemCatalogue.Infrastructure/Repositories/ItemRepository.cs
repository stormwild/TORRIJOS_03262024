using ItemCatalogue.Core.Models;
using ItemCatalogue.Core.Repositories;

using Microsoft.EntityFrameworkCore;

namespace ItemCatalogue.Infrastructure;

public class ItemRepository : IItemRepository
{
    private readonly CatalogueDbContext _db;

    public ItemRepository(CatalogueDbContext db)
    {
        _db = db;
    }

    public async Task<Catalogue?> GetItemsListAsync(CatalogueId catalogueId, CancellationToken ct)
    {
        return await _db.Catalogues
            .Include(c => c.Items)
            .ThenInclude(i => i.PrimaryCategory)
            .FirstOrDefaultAsync(c => c.Id == catalogueId, ct);
    }

    public async Task<Item?> GetItemByIdAsync(CatalogueId catalogueId, ItemId itemId, CancellationToken ct)
    {
        return await _db.Items
            .Include(i => i.PrimaryCategory)
            .FirstOrDefaultAsync(i => i.Id == itemId && i.CatalogueId == catalogueId, ct);
    }

    public async Task<(Item? item, int entries)> CreateItemAsync(Item item, CancellationToken ct)
    {
        var existingItem = await _db.Items.FindAsync(item.Id, ct);

        if (existingItem is not null)
        {
            return (existingItem, 0);
        }

        _db.Items.Add(item);

        var entries = await _db.SaveChangesAsync(ct);

        return (item, entries);
    }
}
