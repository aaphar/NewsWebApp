using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            builder.Property(ct => ct.Status);

            builder.Property(ct => ct.InsertDate);

            builder.Property(ct => ct.PublishDate);

            builder.Property(ct => ct.LanguageId)
                .IsRequired();

            builder.Property(ct => ct.CategoryId)
                .IsRequired();

            builder.HasOne(ct => ct.Language)
                .WithMany(l => l.CategoryTranslations)
                .HasForeignKey(ct => ct.LanguageId);

            builder.HasOne(ct => ct.Category)
                .WithMany(c => c.CategoryTranslations)
                .HasForeignKey(ct => ct.CategoryId);
        }
    }
}
