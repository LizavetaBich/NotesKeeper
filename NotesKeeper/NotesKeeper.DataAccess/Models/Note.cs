using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.DataAccess.Models
{
    public class Note
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public  string Content { get; set; }

        public Tag Tag { get; set; }
    }
}
