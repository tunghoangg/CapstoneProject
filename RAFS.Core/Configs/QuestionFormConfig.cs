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
    public class QuestionFormConfig : IEntityTypeConfiguration<QuestionForm>
    {
        public void Configure(EntityTypeBuilder<QuestionForm> builder)
        {
            builder.ToTable("QuestionForms");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.GuestName).IsRequired().HasMaxLength(100).IsUnicode();

            builder.Property(x => x.Email).IsRequired().HasMaxLength(100).IsUnicode();

            builder.Property(x => x.Title).IsRequired().HasMaxLength(100).IsUnicode();

            builder.Property(x => x.Description).IsRequired().HasMaxLength(int.MaxValue).IsUnicode();

            builder.Property(x => x.Status).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.SendDate).HasDefaultValue(DateTime.UtcNow);
        }
    }
}
