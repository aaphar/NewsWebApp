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

            builder.Property(l => l.Name);

            builder.Property(l => l.LanguageCode);

            builder.OwnsMany(l => l.CategoryTranslations)
                .HasOne(c => c.Language);

            builder.OwnsMany(l => l.PostTranslations)
                .HasOne(p => p.Language);
                        
        }
    }
}
