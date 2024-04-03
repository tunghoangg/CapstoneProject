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
    public class BlogImageConfig : IEntityTypeConfiguration<BlogImage>
    {
        public void Configure(EntityTypeBuilder<BlogImage> builder)
        {
            builder.ToTable("BlogImages");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.URL).IsRequired().HasMaxLength(int.MaxValue).IsUnicode();
            builder.Property(x => x.BlogId).IsRequired();

            builder.HasOne(x => x.Blog)
                .WithMany(x => x.BlogImages)
                .HasForeignKey(x => x.BlogId);
        }
    }
}
