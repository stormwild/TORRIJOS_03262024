using ItemCatalogue.Core.Models;
using ItemCatalogue.Infrastructure;

using Microsoft.EntityFrameworkCore;

using NSubstitute;

namespace ItemCatalogue.Tests;

public class MockDb : IDbContextFactory<CatalogueDbContext>
{
    public CatalogueDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<CatalogueDbContext>()
            .UseSqlite("Data Source=:memory:")
            .Options;

        var ctx = Substitute.For<CatalogueDbContext>();
        // ctx.Set<Catalogue>().Returns(Substitute.For<DbSet<Catalogue>>());
        // ctx.Set<Category>().Returns(Substitute.For<DbSet<Category>>());
        // ctx.Set<Item>().Returns(Substitute.For<DbSet<Item>>());

        // ctx.Catalogues.Returns(Substitute.For<DbSet<Catalogue>>());
        // ctx.Categories.Returns(Substitute.For<DbSet<Category>>());
        // ctx.Items.Returns(Substitute.For<DbSet<Item>>());


        // Create a list of catalogues to be returned when the Catalogues property is accessed
        // var catalogues = new List<Catalogue> { /* your test data here */ }.AsQueryable();

        // // Create a mock DbSet
        // var mockSet = Substitute.For<DbSet<Catalogue>, IQueryable<Catalogue>>();
        // ((IQueryable<Catalogue>)mockSet).Provider.Returns(catalogues.Provider);
        // ((IQueryable<Catalogue>)mockSet).Expression.Returns(catalogues.Expression);
        // ((IQueryable<Catalogue>)mockSet).ElementType.Returns(catalogues.ElementType);
        // ((IQueryable<Catalogue>)mockSet).GetEnumerator().Returns(catalogues.GetEnumerator());
        // ((IQueryable<Catalogue>)mockSet).Async().Returns(catalogues.GetEnumerator());

        // // Set up the Catalogues property to return the mock DbSet
        // ctx.When(c => c.Catalogues) Catalogues.Returns(mockSet);

        return ctx;
    }

}
