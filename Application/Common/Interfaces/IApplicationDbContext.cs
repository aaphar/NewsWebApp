using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Role> Roles { get; }
        DbSet<User> Users { get; }
        DbSet<Domain.Entities.Language> Languages { get; }
        DbSet<Category> Categories { get; }
        DbSet<CategoryTranslation> CategoryTranslations { get; }
        DbSet<Post> Posts { get; }
        DbSet<PostTranslation> PostTranslations { get; }
        DbSet<Hashtag> Hashtags { get; }
        DbSet<PostHashtag> PostHashtags { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
