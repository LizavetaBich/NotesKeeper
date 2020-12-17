using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.Common.Models
{
    public interface ICalendarEvent
    {
        Guid Id { get; set; }

        ICollection<IEventDateTime> EventDateTimes { get; set; }

        string Title { get; set; }

        string Content { get; set; }

        ICollection<ITag> Tags { get; set; }
    }
}
