using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    internal class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(l => l.LanguageCode)
                .IsRequired()
                .HasMaxLength(10);

            builder.HasMany(l => l.CategoryTranslations)
                .WithOne(ct => ct.Language)
                .HasForeignKey(ct => ct.LanguageId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(l => l.PostTranslations)
                .WithOne(pt => pt.Language)
                .HasForeignKey(pt => pt.LanguageId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
