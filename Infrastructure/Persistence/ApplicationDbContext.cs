using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IApplicationDbContext
    {
        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostTranslation> PostTranslations { get; set; }

        public DbSet<Hashtag> Hashtags { get; set; }

        public DbSet<PostHashtag> PostHashtags { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}
