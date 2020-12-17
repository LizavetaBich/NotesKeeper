using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.Common.Models
{
    public interface IEventDateTime
    {
        Guid Id { get; set; }

        ICalendarEvent Event { get; set; }

        DateTime DateTime { get; set; }
    }
}
