using Microsoft.EntityFrameworkCore;
using NotesKeeper.Common;
using NotesKeeper.Common.Interfaces.DataAccess;
using NotesKeeper.Common.Models;
using NotesKeeper.Common.Models.AccountModels;

namespace NotesKeeper.DataAccess.EntityFramework
{
    public class MainDbContext : DbContext, IDbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base (options)
        {
            Database.Migrate();
        }

        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .Ignore(c => c.Token)
                .Property(c => c.Role)
                .HasColumnType("int");

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(c => c.Id)
                .IsUnique();

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.RefreshTokens)
                .WithOne(c => c.User);
        }
    }
}
