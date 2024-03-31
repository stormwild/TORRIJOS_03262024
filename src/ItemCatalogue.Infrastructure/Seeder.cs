using ItemCatalogue.Core.Models;

using MassTransit;

namespace ItemCatalogue.Infrastructure;

public static class Seeder
{
    public static void Initialize(CatalogueDbContext context)
    {
        if (!context.Catalogues.Any())
        {
            var catalogue = new Catalogue { Id = new CatalogueId(NewId.NextGuid()), Name = "Default Catalogue" };
            context.Catalogues.Add(catalogue);
        }

        if (!context.Categories.Any())
        {
            var category = new Category { Id = new CategoryId(NewId.NextGuid()), Name = "Default Category" };
            context.Categories.Add(category);
        }

        context.SaveChanges();
    }

}
