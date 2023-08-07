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
    internal class PostHashtagConfiguration : IEntityTypeConfiguration<PostHashtag>
    {
        public void Configure(EntityTypeBuilder<PostHashtag> builder)
        {
            builder.HasKey(ph => new { ph.NewsId, ph.HashtagId });

            builder.Property(ph => ph.NewsId)
                .IsRequired();

            builder.Property(ph => ph.HashtagId)
                .IsRequired();

            builder.HasOne(ph => ph.PostTranslation)
                .WithMany(pt => pt.PostHashtags)
                .HasForeignKey(ph => ph.NewsId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(ph => ph.Hashtag)
                .WithMany(h => h.PostHashtags)
                .HasForeignKey(ph => ph.HashtagId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
