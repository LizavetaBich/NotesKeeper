using Microsoft.EntityFrameworkCore;
using NotesKeeper.Common.Interfaces;
using NotesKeeper.DataAccess.Interfaces.DbContexts;
using NotesKeeper.DataAccess.Models;
using NotesKeeper.DataAccess.Services.DbContexts.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.DataAccess.Services.DbContexts
{
    internal class MainDbContext : DbContext, IMainDbContext
    {
        private readonly IDbConnectionStringProvider _dbConnectionStringProvider;

        internal MainDbContext(IDbConnectionStringProvider dbConnectionStringProvider)
        {
            _dbConnectionStringProvider = dbConnectionStringProvider;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbConnectionStringProvider.GetMainDbConnectionString());
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
