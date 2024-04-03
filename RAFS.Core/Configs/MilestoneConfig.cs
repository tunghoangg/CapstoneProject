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
    public class MilestoneConfig : IEntityTypeConfiguration<Milestone>
    {
        public void Configure(EntityTypeBuilder<Milestone> builder)
        {
            builder.ToTable("Milestones");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100).IsUnicode();
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.LastUpdate).IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(int.MaxValue).IsUnicode();

            builder.HasOne(f => f.Farm)
                .WithMany(f => f.Milestones)
                .HasForeignKey(f => f.FarmId);

        }
    }
}
