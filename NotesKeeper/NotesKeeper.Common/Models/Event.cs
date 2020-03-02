using NotesKeeper.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.Common
{
    public class CustomEvent
    {
        public CustomEvent()
        {
            Days = new List<Day>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Day> Days { get; }

        public StatusEnum Status { get; set; }

        public string Place { get; set; }
    }
}
