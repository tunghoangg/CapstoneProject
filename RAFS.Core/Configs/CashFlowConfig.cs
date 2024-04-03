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
    public class CashFlowConfig : IEntityTypeConfiguration<CashFlow>
    {
        public void Configure(EntityTypeBuilder<CashFlow> builder)
        {
            builder.ToTable("CashFlows");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).IsRequired();

            builder.Property(x => x.Value).IsRequired();

            builder.Property(x => x.Description).IsRequired().HasMaxLength(int.MaxValue).IsUnicode();

            builder.Property(x => x.Status).IsRequired().HasDefaultValue(false);

            builder.Property(x => x.CreatedTime).IsRequired().HasDefaultValue(DateTime.UtcNow);

            //builder.Property(x => x.TypeId).IsRequired();

            //builder.Property(x => x.FarmId).IsRequired();

            //builder.Property(x => x.UserId).IsRequired();

            builder.HasOne(x => x.TypeCashFlow)
                .WithMany(x => x.CashFlows)
                .HasForeignKey(x => x.TypeId);

            builder.HasOne(x => x.Farm)
                .WithMany(x => x.CashFlows)
                .HasForeignKey(x => x.FarmId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.CashFlows)
                .HasForeignKey(x => x.UserId);
        }
    }
}
