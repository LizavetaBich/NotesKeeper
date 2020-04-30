using Microsoft.EntityFrameworkCore.Infrastructure;
using NotesKeeper.Common.Enums;
using NotesKeeper.Common.Models.AccountModels;
using System.Collections.Generic;

namespace NotesKeeper.Common.Models
{
    public class ApplicationUser : BaseModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public AccessToken Token { get; set; }

        public string DbConnectionString { get; set; }

        public Role Role { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
