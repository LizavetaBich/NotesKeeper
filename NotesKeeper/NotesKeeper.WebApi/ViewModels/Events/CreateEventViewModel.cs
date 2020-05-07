using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesKeeper.WebApi.ViewModels.Events
{
    public class CreateEventViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Place { get; set; }

        public string BackgroundColor { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public bool IsAllDay { get; set; }

        public int Frequency { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public IEnumerable<int> Days { get; set; }
    }
}
