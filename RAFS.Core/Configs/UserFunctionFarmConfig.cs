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
    public class UserFunctionFarmConfig : IEntityTypeConfiguration<UserFunctionFarm>
    {
        public void Configure(EntityTypeBuilder<UserFunctionFarm> builder)
        {
            builder.ToTable("UserFunctionFarms");
            builder.HasKey(uff => new { uff.FarmId, uff.AspUserId, uff.FunctionId });
            builder.Property(f => f.FunctionId).HasDefaultValue(1);
            //builder.HasKey(x => x.AspUserId);
            //builder.HasKey(x => x.FarmId);

            builder.Property(f => f.CreatedDate).IsRequired().HasDefaultValue(DateTime.UtcNow);

            builder.HasOne(f => f.AspUser)
                .WithMany(f => f.UserFunctionFarms)
                .HasForeignKey(f => f.AspUserId);

            builder.HasOne(f => f.Farm)
                .WithMany(f => f.UserFunctionFarms)
                .HasForeignKey(f => f.FarmId);

            builder.HasOne(f => f.Function)
                .WithMany(f => f.UserFunctionFarms)
                .HasForeignKey(f => f.FunctionId);
        }
    }
}
