using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class AttendantConfiguration : IEntityTypeConfiguration<Attendant>
    {
        public void Configure(EntityTypeBuilder<Attendant> builder)
        {
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(15);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(15);
            builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(p => p.Email).IsRequired().HasMaxLength(70);
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.HasMany(p => p.Orders)
                .WithOne(p => p.Attendant)
                .HasForeignKey(p => p.IdAttendant);
        }
    }
}
