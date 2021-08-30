using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasIndex(p => p.Name).IsUnique();
            builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.HasMany(p => p.Attendants)
                .WithOne(p => p.Role)
                .HasForeignKey(p => p.IdRole);
        }
    }
}
