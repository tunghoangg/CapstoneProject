﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RAFS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Configs
{
    public class TypeItemConfig : IEntityTypeConfiguration<TypeItem>
    {
        public void Configure(EntityTypeBuilder<TypeItem> builder)
        {
            builder.ToTable("TypeItems");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(255).IsUnicode();
        }
    }
}
