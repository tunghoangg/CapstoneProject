using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RAFS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Configs
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(e => e.Id);

            builder.Property(x => x.Username).IsRequired().HasMaxLength(255).IsUnicode();

            builder.Property(x => x.Email).IsRequired().HasMaxLength(255).IsUnicode();

            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(10).IsUnicode();

            builder.Property(x => x.Gender).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.Address).IsRequired().HasMaxLength(int.MaxValue).IsUnicode();

            builder.Property(x => x.LastUpdated).IsRequired().HasDefaultValue(DateTime.UtcNow);

            builder.Property(x => x.Status).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.FarmId).IsRequired();

            builder.HasOne(x => x.Farm)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.FarmId);
        }
    }
}
