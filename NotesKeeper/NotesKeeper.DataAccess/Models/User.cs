using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.DataAccess.Models
{
    public class User : BaseItem
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string ConnectionString { get; set; }
    }
}
