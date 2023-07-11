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
    internal class PostTranslationConfiguration : IEntityTypeConfiguration<PostTranslation>
    {
        public void Configure(EntityTypeBuilder<PostTranslation> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(pt => pt.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(pt => pt.Context)
                .IsRequired();

            builder.Property(p => p.InsertDate);

            builder.Property(p => p.PublishDate);

            builder.Property(p => p.Status);

            builder.Property(p => p.ViewCount);

            builder.Property(pt => pt.LanguageId)
                .IsRequired();

            builder.Property(pt => pt.NewsId)
                .IsRequired();

            builder.Property(pt => pt.AuthorId)
                .IsRequired();

            builder.HasOne(p => p.Language)
                .WithMany(l => l.PostTranslations)
                .HasForeignKey(p => p.LanguageId);

            builder.HasOne(p => p.Post)
                .WithMany(po => po.PostTranslations)
                .HasForeignKey(p => p.NewsId);

            builder.HasOne(p => p.Author)
                .WithMany(u => u.PostTranslations)
                .HasForeignKey(p => p.AuthorId);
        }
    }
}
