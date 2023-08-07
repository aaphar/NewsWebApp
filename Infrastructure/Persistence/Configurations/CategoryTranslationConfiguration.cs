using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Infrastructure.Persistence.Configurations
{
    internal class CategoryTranslationConfiguration : IEntityTypeConfiguration<CategoryTranslation>
    {
        public void Configure(EntityTypeBuilder<CategoryTranslation> builder)
        {
            builder.HasKey(ct => ct.Id);

            builder.Property(ct => ct.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(ct => ct.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (Status)Enum.Parse(typeof(Status), v));

            builder.Property(ct => ct.InsertDate);

            builder.Property(ct => ct.PublishDate);

            builder.Property(ct => ct.LanguageId);

            builder.Property(ct => ct.CategoryId);


            builder.HasOne(ct => ct.Language)
                .WithMany(l => l.CategoryTranslations)
                .HasForeignKey(ct => ct.LanguageId)
                .OnDelete(DeleteBehavior.SetNull);


            builder.HasOne(ct => ct.Category)
                .WithMany(c => c.CategoryTranslations)
                .HasForeignKey(ct => ct.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
