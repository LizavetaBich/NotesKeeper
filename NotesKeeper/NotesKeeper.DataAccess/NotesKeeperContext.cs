using System;
using Microsoft.EntityFrameworkCore;
using NotesKeeper.Common.Models;

namespace NotesKeeper.DataAccess
{
    public class NotesKeeperContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Note> Notes { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public NotesKeeperContext(string connectionString)
        {
            this._connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(this._connectionString);
        }
    }
}
