using NotesKeeper.Common.Models;
using NotesKeeper.Common.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NotesKeeper.BusinessLayer
{
    public interface ITokenService
    {
        bool ValidateAccessToken(AccessToken token);

        AccessToken GenerateAccessToken(ApplicationUser user);

        bool ValidateRefreshToken(RefreshToken token, Guid userId);

        RefreshToken GenerateRefreshToken();

        Guid GetUserIdFromAccessToken(AccessToken accessToken);

        Guid GetUserIdFromClaimsPrincipal(ClaimsPrincipal claimsPrincipal);
    }
}
