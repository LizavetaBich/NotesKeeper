using NotesKeeper.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesKeeper.WebApi.ViewModels
{
    public class CalendarEventViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Place { get; set; }

        public DateTime EventStartTime { get; set; }

        public DateTime EventEndTime { get; set; }

        public ICollection<DateTime> Days { get; set; } = new List<DateTime>();

        public bool AllDay { get; set; }

        public string BackgroundColor { get; set; }
    }
}
