using System;

namespace NotesKeeper.Common.Models
{
    public class Note : BaseItem
    {
        public string Name { get; set; }

        public string Content { get; set; }

        public bool IsEnabled { get; set; }

        public DateTime EnabledFrom { get; set; }

        public DateTime Expired { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
