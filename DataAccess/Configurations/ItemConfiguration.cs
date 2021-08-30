using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasIndex(p => p.Name).IsUnique();
            builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Price).HasMaxLength(5).IsRequired();
            builder.HasMany(p => p.OrderItems)
                .WithOne(p => p.Item)
                .HasForeignKey(p => p.IdItem);
        }
    }
}
