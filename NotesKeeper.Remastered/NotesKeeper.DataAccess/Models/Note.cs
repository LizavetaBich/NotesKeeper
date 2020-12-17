using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NotesKeeper.DataAccess.Models
{
    public class Note
    {
        public Guid Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
