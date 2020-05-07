using System;
using System.Collections.Generic;

namespace NotesKeeper.Common
{
    public class Day : BaseModel
    {
        public DateTime Date { get; set; }

        public virtual ICollection<EventDay> DayEvents { get; set; } = new List<EventDay>();

        public static implicit operator Day(DateTime dateTime)
        {
            return new Day { 
                Id = Guid.NewGuid(), 
                Date = dateTime,
                CreatedDate = DateTime.Now
            };
        }
    }
}
