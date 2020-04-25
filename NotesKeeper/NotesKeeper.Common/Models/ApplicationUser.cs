using NotesKeeper.Common.Enums;

namespace NotesKeeper.Common.Models
{
    public class ApplicationUser : BaseModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Token { get; set; }

        public string DbConnectionString { get; set; }

        public Role Role { get; set; }
    }
}
