using System;

namespace NotesKeeper.Common
{
    public class EventDay
    {
        public Guid Id { get; set; }

        public Guid EventId { get; set; }

        public virtual CustomEvent Event { get; set; }

        public Guid DayId { get; set; }

        public virtual Day Day { get; set; }
    }
}
