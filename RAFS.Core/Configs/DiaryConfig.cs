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
    public class DiaryConfig : IEntityTypeConfiguration<Diary>
    {
        public void Configure(EntityTypeBuilder<Diary> builder)
        {
            builder.ToTable("Diaries");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).IsRequired().HasMaxLength(50).IsUnicode();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Body).HasMaxLength(200).IsUnicode();
            builder.Property(x => x.CreatedDay).IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.Image).HasMaxLength(250);

            builder.Property(x => x.Status).IsRequired().HasDefaultValue(true);
            builder.Property(x => x.LastUpdate).IsRequired().HasDefaultValue(DateTime.UtcNow);

            builder.HasOne(f => f.Plant)
                .WithMany(f => f.Diaries)
                .HasForeignKey(f => f.PlantId);
        }
    }
}
