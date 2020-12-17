using Microsoft.EntityFrameworkCore;
using NotesKeeper.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.DataAccess.Interfaces.DbContexts
{
    internal interface IUserDbContext
    {
        DbSet<Note> Notes { get; set; }

        DbSet<CalendarEvent> Events { get; set; }

        DbSet<EventDateTime> EventDates { get; set; }

        DbSet<Tag> Tags { get; set; }
    }
}
