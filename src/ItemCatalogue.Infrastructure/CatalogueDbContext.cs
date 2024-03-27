using ItemCatalogue.Core.Models;
using ItemCatalogue.Infrastructure.Configurations;

using Microsoft.EntityFrameworkCore;

namespace ItemCatalogue.Infrastructure;

public class CatalogueDbContext : DbContext
{
    public CatalogueDbContext(DbContextOptions<CatalogueDbContext> options) : base(options) { }

    public DbSet<Catalogue> Catalogues { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Item> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source=Db/catalogue.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CatalogueConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ItemConfiguration());
    }
}
