using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>, IApplicationDbContext
    {
        public DbSet<Role> MyRoles => Set<Role>();

        public DbSet<User> MyUsers => Set<User>();
        
        public override DbSet<ApplicationRole> Roles => Set<ApplicationRole>();

        public override DbSet<ApplicationUser> Users => Set<ApplicationUser>();

        public DbSet<Language> Languages => Set<Language>();

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<CategoryTranslation> CategoryTranslations => Set<CategoryTranslation>();

        public DbSet<Post> Posts => Set<Post>();

        public DbSet<PostTranslation> PostTranslations => Set<PostTranslation>();

        public DbSet<Hashtag> Hashtags => Set<Hashtag>();

        public DbSet<PostHashtag> PostHashtags => Set<PostHashtag>();

        private IConfiguration? Configuration { get; }

       
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
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


            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationRole>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Ignore<IdentityUserRole<int>>();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
