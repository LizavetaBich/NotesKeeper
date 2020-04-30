using NotesKeeper.Common.Models.AccountModels;

namespace NotesKeeper.WebApi.ViewModels
{
    public class RefreshAccessTokenViewModel
    {
        public RefreshTokenViewModel RefreshToken { get; set; }

        public AccessToken AccessToken { get; set; }
    }
}
