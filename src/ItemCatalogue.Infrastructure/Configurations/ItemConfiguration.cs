using ItemCatalogue.Core.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItemCatalogue.Infrastructure.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
               .HasConversion(i => i.Value, i => new ItemId(i));

        builder.Property(i => i.Name)
               .IsRequired();

        // Add any additional configuration for the Item entity here
        builder.HasOne(i => i.PrimaryCategory)
         .WithMany()
         .HasForeignKey(i => i.PrimaryCategoryId)
         .IsRequired();

        builder.HasMany(i => i.Categories)
               .WithMany()
               .UsingEntity<Dictionary<string, object>>(
                    "ItemCategory",
                    j => j.HasOne<Category>().WithMany().HasForeignKey("CategoryId"),
                    j => j.HasOne<Item>().WithMany().HasForeignKey("ItemId")
                );
    }
}
