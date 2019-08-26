using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NotesKeeper.DataAccess.Models;

namespace NotesKeeper.DataAccess
{
    public class NotesKeeperMasterContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public NotesKeeperMasterContext(DbContextOptions<NotesKeeperMasterContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
