using NotesKeeper.Common.Models;
using System;

namespace NotesKeeper.DataAccess.Models
{
    public class User : IUser
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }
    }
}
