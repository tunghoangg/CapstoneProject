using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RAFS.Core.DTO;
using RAFS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Configs
{
    public class AspUserConfig : IEntityTypeConfiguration<AspUser>
    {
        public void Configure(EntityTypeBuilder<AspUser> builder)
        {
            //Của AspUsser
            builder.Property(x => x.EmailConfirmed).HasDefaultValue(false);
            builder.Property(x => x.PhoneNumberConfirmed).HasDefaultValue(false);
            builder.Property(x => x.LockoutEnabled).HasDefaultValue(false);
            builder.Property(x => x.TwoFactorEnabled).HasDefaultValue(false);
            builder.Property(x => x.AccessFailedCount).HasDefaultValue(0);

            //Tự tạo
            builder.Property(x => x.LastUpdated).HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(true);
            builder.Property(x => x.Avatar).IsRequired(false).HasMaxLength(int.MaxValue).HasDefaultValue("https://lh3.googleusercontent.com/d/1LtjBZGYa-Mn6n1D7n2WwXwLrRpeUIUkY").IsUnicode();
            builder.Property(x => x.FullName).IsRequired(false).HasMaxLength(255).IsUnicode();
            builder.Property(x => x.Address).IsRequired(false).HasMaxLength(255).IsUnicode();
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(int.MaxValue).IsUnicode(); 


        }

        public static void CreateMap(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<RegistrationDTO, AspUser>();
            cfg.CreateMap<AspUser, RegistrationDTO>();
        }
    }
}
