using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class PizzeriaHallConfiguration : IEntityTypeConfiguration<PizzeriaHall>
    {
        public void Configure(EntityTypeBuilder<PizzeriaHall> builder)
        {
            builder.HasIndex(p => p.Name).IsUnique(); 
            builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.HasMany(p => p.Tables)
                .WithOne(p => p.PizzeriaHall)
                .HasForeignKey(p => p.IdPizzeriaHall);
        }
    }
}
