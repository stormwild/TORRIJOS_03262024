using ItemCatalogue.Core.Models;

namespace ItemCatalogue.Infrastructure.Repositories;

public interface ICategoryRepository
{
    Task<Category?> GetCategoryAsync(CategoryId categoryId, CancellationToken ct);
    Task<List<Category>> GetCategoriesByIdsAsync(IEnumerable<CategoryId> categoryIds, CancellationToken ct);
}