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
    public class FarmConfig : IEntityTypeConfiguration<Farm>
    {
        public void Configure(EntityTypeBuilder<Farm> builder)
        {
            builder.ToTable("Farms");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Code).IsUnique();
            builder.Property(x => x.Code).IsRequired().HasMaxLength(10).IsUnicode();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(255).IsUnicode();

            builder.Property(x => x.Address).IsRequired().HasMaxLength(255).IsUnicode();

            builder.Property(x => x.Phone).IsRequired().HasMaxLength(10);

            builder.Property(x => x.Logo).IsRequired().HasMaxLength(int.MaxValue).IsUnicode().HasDefaultValue("https://lh3.googleusercontent.com/d/1-cD42GWStpz0_4kJDCSSHOgFOwcDX3ik");

            builder.Property(x => x.PageLink).HasMaxLength(int.MaxValue).IsUnicode();

            builder.Property(x => x.Area).IsRequired().HasDefaultValue(0);

            builder.Property(x => x.EstablishedDate).IsRequired().HasDefaultValue(DateTime.UtcNow);

            builder.Property(x => x.CreatedDate).IsRequired().HasDefaultValue(DateTime.UtcNow);

            builder.Property(x => x.Status).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.Description).HasMaxLength(int.MaxValue).IsUnicode();

            builder.Property(x => x.LastUpdate).IsRequired().HasDefaultValue(DateTime.UtcNow);

        }
    }
}
