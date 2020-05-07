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

        public DbSet<Day> Days { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomEvent>()
                .HasIndex(c => c.Id)
                .IsUnique();

            modelBuilder.Entity<EventDay>()
                .HasOne(sc => sc.Day)
                .WithMany(s => s.DayEvents)
                .HasForeignKey(sc => sc.DayId);

            modelBuilder.Entity<EventDay>()
                .HasOne(sc => sc.Event)
                .WithMany(c => c.EventDays)
                .HasForeignKey(sc => sc.EventId);
        }
    }
}
