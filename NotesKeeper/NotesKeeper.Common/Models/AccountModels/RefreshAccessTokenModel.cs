using NotesKeeper.Common.Models.AccountModels;

namespace NotesKeeper.Common.Models.AccountModels
{
    public class RefreshAccessTokenModel
    {
        public RefreshToken RefreshToken { get; set; }

        public AccessToken AccessToken { get; set; }
    }
}
