using System;

namespace NotesKeeper.Common.Models
{
    public interface ITag
    {
        Guid Id { get; set; }

        string Title { get; set; }

        string ColorCode { get; set; }
    }
}
