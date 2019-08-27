using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.DataAccess.Models
{
    public class Tag : BaseItem
    {
        public string Content { get; set; }

        public string Color { get; set; }
    }
}
