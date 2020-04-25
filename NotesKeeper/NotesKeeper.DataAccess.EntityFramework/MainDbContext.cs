using Microsoft.EntityFrameworkCore;
using NotesKeeper.Common;
using NotesKeeper.Common.Interfaces.DataAccess;
using NotesKeeper.Common.Models;

namespace NotesKeeper.DataAccess.EntityFramework
{
    public class MainDbContext : DbContext, IDbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base (options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .Ignore(c => c.Token)
                .Property(c => c.Role)
                .HasColumnType("int");

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(c => c.Id)
                .IsUnique();
        }
    }
}
