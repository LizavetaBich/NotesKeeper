using NotesKeeper.Common.Enums;
using NotesKeeper.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.Common
{
    public class CustomEvent: BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public StatusEnum Status { get; set; }

        public DateTime EventStartTime { get; set; }

        public DateTime EventLastTime { get; set; }

        public string Place { get; set; }

        public bool AllDay { get; set; }

        public string BackgroundColor { get; set; }

        public virtual ICollection<EventDay> EventDays { get; set; } = new List<EventDay>(); 
    }
}
