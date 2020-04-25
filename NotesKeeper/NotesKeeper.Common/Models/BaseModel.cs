using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.Common
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
