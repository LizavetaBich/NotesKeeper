using NotesKeeper.Common.Models;
using System;

namespace NotesKeeper.DataAccess.Models
{
    public class Tag : ITag
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string ColorCode { get; set; }
    }
}
