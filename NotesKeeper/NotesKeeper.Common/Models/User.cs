using System;
using Microsoft.AspNetCore.Identity;

namespace NotesKeeper.Common.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ConnectionString { get; set; }

        public DateTime DeletionDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
