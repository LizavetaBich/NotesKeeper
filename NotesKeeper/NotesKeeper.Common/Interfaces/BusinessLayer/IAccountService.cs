using NotesKeeper.Common.Models;
using NotesKeeper.Common.Models.AccountModels;
using System.Threading.Tasks;

namespace NotesKeeper.Common.Interfaces.BusinessLayer
{
    public interface IAccountService
    {
        Task<ApplicationUser> RegisterUser(RegisterModel registerModel);

        Task<(ApplicationUser User, RefreshToken RefreshToken)> LoginUser(LoginModel loginModel);

        Task<RefreshAccessTokenModel> RefreshAccessToken(RefreshAccessTokenModel model);
    }
}
