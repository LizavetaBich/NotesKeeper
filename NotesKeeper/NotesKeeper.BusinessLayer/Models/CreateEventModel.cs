using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.BusinessLayer.Models
{
    public class CreateEventModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Place { get; set; }

        public string BackgroundColor { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public bool IsAllDay { get; set; }

        public int Frequency { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public IEnumerable<int> Days { get; set; }
    }
}
