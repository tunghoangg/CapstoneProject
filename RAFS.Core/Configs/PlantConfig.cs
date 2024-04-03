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
    public class PlantConfig : IEntityTypeConfiguration<Plant>
    {
        
        public void Configure(EntityTypeBuilder<Plant> builder)
        {
            builder.ToTable("Plants");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50).IsUnicode();
            builder.Property(x => x.Type).IsRequired().HasMaxLength(20).IsUnicode();
            builder.Property(x => x.Description).HasMaxLength(200).IsUnicode();
            builder.Property(x => x.Area).IsRequired();
            builder.Property(x => x.AreaUnit).HasMaxLength(20);
            builder.Property(x => x.HealthCondition);
            builder.Property(x => x.PlantingMethod).HasMaxLength(50).IsUnicode();
            builder.Property(x => x.Image).HasMaxLength(250);
            builder.Property(x => x.QRCode).IsRequired().HasMaxLength(250);

            builder.Property(x => x.Status).IsRequired().HasDefaultValue(1);

            builder.Property(x => x.LastUpdate).IsRequired().HasDefaultValue(DateTime.UtcNow);

            builder.HasOne(f => f.Milestone)
                .WithMany(f => f.Plants)
                .HasForeignKey(f => f.MilestoneId);

        }
    }
}
