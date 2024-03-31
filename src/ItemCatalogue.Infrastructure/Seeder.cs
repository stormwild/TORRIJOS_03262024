using ItemCatalogue.Core.Models;

using MassTransit;

using Microsoft.EntityFrameworkCore;

namespace ItemCatalogue.Infrastructure;

public static class Seeder
{
    public static Guid GetDefaultCatalogueId()
    {
        // 88BD0000-F588-04D9-3C54-08DC4E40D836 // current value in db file
        var ids = NewId.NextGuid([Guid.Parse("88BD0000-F588-04D9-3C54-08DC4E40D836")], 0, 1);
        var defaultId = ids.Single();
        return defaultId;
    }

    public static Guid GetDefaultCategoryId()
    {
        // 88BD0000-F588-04D9-F9CD-08DC4E40D83E // current value in db file
        var ids = NewId.NextGuid([Guid.Parse("88BD0000-F588-04D9-F9CD-08DC4E40D83E")], 0, 1);
        var defaultId = ids.Single();
        return defaultId;
    }


    public static async Task Initialize(CatalogueDbContext context)
    {
        if (!context.Catalogues.Any())
        {
            var catalogue = new Catalogue { Id = new CatalogueId(GetDefaultCatalogueId()), Name = "Default Catalogue" };
            context.Catalogues.Add(catalogue);
        }

        if (!context.Categories.Any())
        {
            var category = new Category { Id = new CategoryId(GetDefaultCategoryId()), Name = "Default Category" };
            context.Categories.Add(category);
        }

        await context.SaveChangesAsync();

        var defaultCatalogue = await context.Catalogues.Include(c => c.Items).FirstOrDefaultAsync();
        var defaultCategory = await context.Categories.FirstOrDefaultAsync();

        if (defaultCatalogue is not null && defaultCategory is not null && defaultCatalogue.Items.Count == 0)
        {
            var items = new List<Item> {
                new() {
                    Id = new ItemId(NewId.NextSequentialGuid()),
                    CatalogueId = defaultCatalogue.Id,
                    Name = "Cool Widget A",
                    PrimaryCategoryId = defaultCategory.Id,
                    PrimaryCategory = defaultCategory
                },
                new() {
                    Id = new ItemId(NewId.NextSequentialGuid()),
                    CatalogueId = defaultCatalogue.Id,
                    Name = "Cool Widget B",
                    PrimaryCategoryId = defaultCategory.Id,
                    PrimaryCategory = defaultCategory
                },
                new() {
                    Id = new ItemId(NewId.NextSequentialGuid()),
                    CatalogueId = defaultCatalogue.Id,
                    Name = "Cool Widget C",
                    PrimaryCategoryId = defaultCategory.Id,
                    PrimaryCategory = defaultCategory
                },
                new() {
                    Id = new ItemId(NewId.NextSequentialGuid()),
                    CatalogueId = defaultCatalogue.Id,
                    Name = "Cool Widget D",
                    PrimaryCategoryId = defaultCategory.Id,
                    PrimaryCategory = defaultCategory
                },
                new() {
                    Id = new ItemId(NewId.NextSequentialGuid()),
                    CatalogueId = defaultCatalogue.Id,
                    Name = "Cool Widget E",
                    PrimaryCategoryId = defaultCategory.Id,
                    PrimaryCategory = defaultCategory
                },
                new() {
                    Id = new ItemId(NewId.NextSequentialGuid()),
                    CatalogueId = defaultCatalogue.Id,
                    Name = "Cool Widget F",
                    PrimaryCategoryId = defaultCategory.Id,
                    PrimaryCategory = defaultCategory
                }
            };

            defaultCatalogue.Items.AddRange(items);

            await context.SaveChangesAsync();
        }
    }

}
