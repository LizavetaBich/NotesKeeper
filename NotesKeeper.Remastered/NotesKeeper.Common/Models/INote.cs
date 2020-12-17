using System;
using System.Collections.Generic;

namespace NotesKeeper.Common.Models
{
    public interface INote
    {
        Guid Id { get; set; }

        string Content { get; set; }

        string Title { get; set; }

        ICollection<ITag> Tags { get; set; }
    }
}
