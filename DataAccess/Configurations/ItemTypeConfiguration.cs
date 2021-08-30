using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class ItemTypeConfiguration : IEntityTypeConfiguration<ItemType>
    {
        public void Configure(EntityTypeBuilder<ItemType> builder)
        {
            builder.HasIndex(p => p.Name).IsUnique(); 
            builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.HasKey(p => p.Id);
            builder.HasMany(p => p.Items)
                .WithOne(p => p.ItemType)
                .HasForeignKey(p => p.IdItemType);
        }
    }
}
