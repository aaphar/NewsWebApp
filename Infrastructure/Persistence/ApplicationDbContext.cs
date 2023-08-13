using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>, IApplicationDbContext
    {
        public DbSet<Language> Languages => Set<Language>();

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<CategoryTranslation> CategoryTranslations => Set<CategoryTranslation>();

        public DbSet<Post> Posts => Set<Post>();

        public DbSet<PostTranslation> PostTranslations => Set<PostTranslation>();

        public DbSet<Hashtag> Hashtags => Set<Hashtag>();

        public DbSet<PostHashtag> PostHashtags => Set<PostHashtag>();


        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IConfiguration configuration) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-N56OGLT\SQLEXPRESS; Database=NewsCleanDB; Trusted_Connection=True; Encrypt=False; TrustServerCertificate=True");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new PostTranslationConfiguration());
            modelBuilder.ApplyConfiguration(new PostHashtagConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
            modelBuilder.ApplyConfiguration(new HashtagConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryTranslationConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
