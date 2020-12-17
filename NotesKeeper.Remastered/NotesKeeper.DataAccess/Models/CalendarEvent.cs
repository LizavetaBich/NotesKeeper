using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.DataAccess.Models
{
    public class CalendarEvent
    {
        public Guid Id { get; set; }

        public virtual ICollection<EventDateTime> EventDateTimes { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
