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
    public class ImageConfig : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Images");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.URL).IsRequired().HasMaxLength(int.MaxValue).IsUnicode();
            builder.Property(x => x.FarmId).IsRequired();

            builder.HasOne(x => x.Farm)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.FarmId);
        }
    }
}
