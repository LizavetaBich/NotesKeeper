using Microsoft.EntityFrameworkCore;
using NotesKeeper.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.DataAccess.Interfaces.DbContexts
{
    public interface IMainDbContext
    {
        DbSet<User> Users { get; set; }
    }
}
