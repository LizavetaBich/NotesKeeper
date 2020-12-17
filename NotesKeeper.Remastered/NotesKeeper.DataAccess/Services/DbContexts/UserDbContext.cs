using Microsoft.EntityFrameworkCore;
using NotesKeeper.Common.Interfaces;
using NotesKeeper.DataAccess.Interfaces.DbContexts;
using NotesKeeper.DataAccess.Models;
using NotesKeeper.DataAccess.Services.DbContexts.Configuration;

namespace NotesKeeper.DataAccess.Services.DbContexts
{
    internal class UserDbContext : DbContext, IUserDbContext
    {
        private readonly IDbConnectionStringProvider _dbConnectionStringProvider;

        internal UserDbContext(IDbConnectionStringProvider dbConnectionStringProvider)
        {
            _dbConnectionStringProvider = dbConnectionStringProvider;
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<CalendarEvent> Events { get; set; }
        public DbSet<EventDateTime> EventDates { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbConnectionStringProvider.GetUserDbConnectionString());
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new NoteConfiguration());
            modelBuilder.ApplyConfiguration(new CalendarEventConfiguration());
            modelBuilder.ApplyConfiguration(new EventDateTimeConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
        }
    }
}
