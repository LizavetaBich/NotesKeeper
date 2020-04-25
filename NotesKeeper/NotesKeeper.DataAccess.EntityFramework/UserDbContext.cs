using Microsoft.EntityFrameworkCore;
using NotesKeeper.Common;
using NotesKeeper.Common.Interfaces.DataAccess;

namespace NotesKeeper.DataAccess.EntityFramework
{
    public class UserDbContext : DbContext, IUserDbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<CustomEvent> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomEvent>()
                .Ignore(c => c.Days)
                .HasIndex(c => c.Id)
                .IsUnique();
        }
    }
}
