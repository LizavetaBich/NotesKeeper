using System;

namespace NotesKeeper.Common.Models
{
    public abstract class BaseItem
    {
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime DeletionDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
