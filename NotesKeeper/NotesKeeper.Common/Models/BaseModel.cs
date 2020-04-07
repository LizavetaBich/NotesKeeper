using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.Common
{
    public abstract class BaseModel
    {
        public BaseModel(Guid id)
        {
            Id = id;
            CreatedDate = DateTime.Now;
        }

        public Guid Id { get; }

        public DateTime CreatedDate { get; }
    }
}
