using NotesKeeper.Common.Enums;
using NotesKeeper.Common.Models.AccountModels;

namespace NotesKeeper.WebApi.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public AccessToken AccessToken { get; set; }

        public RefreshTokenViewModel RefreshToken { get; set; }

        public Role Role { get; set; }
    }
}
