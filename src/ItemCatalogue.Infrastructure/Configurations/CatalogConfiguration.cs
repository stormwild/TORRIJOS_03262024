using ItemCatalogue.Core.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItemCatalogue.Infrastructure.Configurations;

public class CatalogueConfiguration : IEntityTypeConfiguration<Catalogue>
{
    public void Configure(EntityTypeBuilder<Catalogue> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
               .HasConversion(c => c.Value, c => new CatalogueId(c));

        builder.HasMany(c => c.Items)
               .WithOne()
               .HasForeignKey(i => i.CatalogueId)
               .IsRequired();
    }
}
