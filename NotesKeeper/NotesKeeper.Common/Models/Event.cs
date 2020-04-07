using NotesKeeper.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.Common
{
    public class CustomEvent: BaseModel
    {
        public CustomEvent(Guid id) : base(id)
        {
            Days = new List<DateTime>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public StatusEnum Status { get; set; }

        public FrequencyEnum Frequency { get; set; }

        public DateTime EventStartDay { get; set; }

        public DateTime? EventLastDay { get; set; }

        public string Place { get; set; }

        public ICollection<DateTime> Days { get; }
    }
}
