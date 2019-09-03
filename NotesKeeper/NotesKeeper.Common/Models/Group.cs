using System.Collections.Generic;

namespace NotesKeeper.Common.Models
{
    public class Group : BaseItem
    {
        public string Name { get; set; }

        public virtual IList<Note> Notes { get; set; }
    }
}
