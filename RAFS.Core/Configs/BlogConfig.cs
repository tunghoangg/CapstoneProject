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
    public class BlogConfig : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {

            builder.ToTable("Blogs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.AuthorId).IsRequired();
            
            builder.Property(x => x.Title).IsRequired().HasMaxLength(60).IsUnicode();

            builder.Property(x => x.Body).IsRequired().HasMaxLength(int.MaxValue).IsUnicode();

            builder.Property(x => x.CreatedDate).IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.LastUpdated).IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(true);

            builder.HasOne(x => x.Author)
                .WithMany(x => x.Blogs)
                .HasForeignKey(x => x.AuthorId);
        }

    }
}
