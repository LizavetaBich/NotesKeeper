using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.DataAccess.Models
{
    public class EventDateTime
    {
        public Guid Id { get; set; }

        public CalendarEvent Event { get; set; }

        public DateTime DateTime { get; set; }
    }
}
