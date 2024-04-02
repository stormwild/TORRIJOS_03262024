using ItemCatalogue.Core.Models;

using Microsoft.EntityFrameworkCore;

namespace ItemCatalogue.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly CatalogueDbContext _db;

    public CategoryRepository(CatalogueDbContext db)
    {
        _db = db;
    }


    public async Task<Category?> GetCategoryAsync(CategoryId categoryId, CancellationToken ct)
    {
        return await _db.Categories.FindAsync(categoryId, ct);
    }

    public async Task<List<Category>> GetCategoriesByIdsAsync(IEnumerable<CategoryId> categoryIds, CancellationToken ct)
    {
        return await _db.Categories.Where(c => categoryIds.Contains(c.Id)).ToListAsync(ct);
    }
}
