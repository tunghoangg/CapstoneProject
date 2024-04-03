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
    public class TakeAndSendMaterialConfig : IEntityTypeConfiguration<TakeAndSendMaterial>
    {
        public void Configure(EntityTypeBuilder<TakeAndSendMaterial> builder)
        {
            builder.ToTable("TakeAndSendMaterials");

            builder.HasKey(x => x.Id);
            //builder.HasKey(x => new { x.InventoryId, x.PlantId });

            builder.Property(x => x.PlantId).IsRequired();
            builder.Property(x => x.InventoryId).IsRequired();

            builder.Property(x => x.Quality).IsRequired().HasMaxLength(50).IsUnicode();


            builder.Property(x => x.Status).IsRequired().HasDefaultValue(true);
            builder.Property(x => x.LastUpdate).IsRequired().HasDefaultValue(DateTime.UtcNow);

            builder.HasOne(f => f.Plant)
                .WithMany(f => f.TakeAndSendMaterials)
                .HasForeignKey(f => f.PlantId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(f => f.Inventory)
                .WithMany(f => f.TakeAndSendMaterials)
                .HasForeignKey(f => f.InventoryId).OnDelete(DeleteBehavior.NoAction);
            
        }
    }
}
