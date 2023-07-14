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
    internal class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(p => p.InsertDate);

            builder.Property(p => p.PublishDate);

            builder.Property(p => p.CategoryId)
                .IsRequired();

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CategoryId);

            builder.HasMany(l => l.PostTranslations)
                .WithOne(pt => pt.Post)
                .HasForeignKey(pt => pt.NewsId);
        }
    }
}
