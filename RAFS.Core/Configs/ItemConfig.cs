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
    public class ItemConfig : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items");
            builder.HasKey(x => x.Id);

            //builder.Property(x => x.ItemCode).IsRequired();

            builder.Property(x => x.ItemName).IsRequired().HasMaxLength(255).IsUnicode();

            builder.Property(x => x.Quantity).IsRequired();

            builder.Property(x => x.UnitId).IsRequired();

            builder.Property(x => x.Value).IsRequired();

            builder.Property(x => x.CreatedTime).IsRequired().HasDefaultValue(DateTime.UtcNow);

            builder.Property(x => x.Description).IsRequired().HasMaxLength(int.MaxValue).IsUnicode();

            builder.Property(x => x.LastUpdate).IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(1);

            //builder.Property(x => x.TypeId).IsRequired();

            //builder.Property(x => x.FarmId).IsRequired();

            builder.HasOne(x => x.Unit)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.UnitId);

            builder.HasOne(x => x.TypeItem)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.TypeId);

            //builder.HasOne(x => x.Farm)
            //    .WithOne(x => x.Inventory)
            //    .HasForeignKey<Inventory>(x => x.FarmId);

            builder.HasOne(x => x.Farm)
                .WithMany(x => x.Inventories)
                .HasForeignKey(x => x.FarmId);
        }
    }
}
