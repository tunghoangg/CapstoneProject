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
    public class BlogTagConfig : IEntityTypeConfiguration<BlogTag>
    {
        public void Configure(EntityTypeBuilder<BlogTag> builder)
        {
            builder.ToTable("BlogTags");
            builder.HasKey(t => new { t.TagId, t.BlogId });

            builder.HasOne(f => f.Tag)
               .WithMany(f => f.BlogTags)
               .HasForeignKey(f => f.TagId);

            builder.HasOne(f => f.Blog)
                .WithMany(f => f.BlogTags)
                .HasForeignKey(f => f.BlogId);
        }
    }
}
