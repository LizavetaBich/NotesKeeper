using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NotesKeeper.Common;
using NotesKeeper.Common.ExtensionMethods;
using NotesKeeper.Common.Interfaces.DataAccess;
using NotesKeeper.Common.Models;
using NotesKeeper.Common.Models.AccountModels;

namespace NotesKeeper.BusinessLayer
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbContext _masterContext;

        public TokenService(IConfiguration configuration, IDbContext dbContext)
        {
            _configuration = configuration;
            _masterContext = dbContext;
        }

        public AccessToken GenerateAccessToken(ApplicationUser user)
        {
            Guard.IsNotNull(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetAppSettingsSection().GetValue<string>("Secret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return new AccessToken
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = tokenDescriptor.Expires.Value
            };
        }

        public RefreshToken GenerateRefreshToken()
        {
            // Create the refresh token
            RefreshToken refreshToken = new RefreshToken()
            {
                Token = GenerateRandomToken(),
                ExpirationTime = DateTime.UtcNow.AddMinutes(35) // Make this configurable
            };

            return refreshToken;
        }

        public Guid GetUserIdFromAccessToken(AccessToken accessToken)
        {
            var claims = GetClaimsFromAccessToken(accessToken);
            return GetUserIdFromClaimsPrincipal(claims);
        }

        public Guid GetUserIdFromClaimsPrincipal(ClaimsPrincipal claimsPrincipal)
        {
            var idClaim = claimsPrincipal.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Sid);

            return Guid.Parse(idClaim.Value);
        }

        public bool ValidateAccessToken(AccessToken token)
        {
            Guard.IsNotNull(token);

            var claims = GetClaimsFromAccessToken(token);

            if (claims == null)
            {
                return false;
            }

            var isClaimsValid = ValidateClaim(ClaimTypes.Sid, claims, out var idClaim)
                && ValidateClaim(ClaimTypes.Email, claims, out var emailClaim)
                && ValidateClaim(ClaimTypes.Role, claims, out var roleClaim);

            var isUserExists = _masterContext.Users
                .Any(x => x.Id == Guid.Parse(idClaim.Value));

            return isClaimsValid && isUserExists;
        }

        public bool ValidateRefreshToken(RefreshToken token, Guid userId)
        {
            return token.ExpirationTime > DateTime.UtcNow && _masterContext
                .Users
                .SingleOrDefault(x => x.Id == userId)
                .RefreshTokens
                .Any(x => x.Token == token.Token && x.ExpirationTime == token.ExpirationTime);
        }

        private string GenerateRandomToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private bool ValidateClaim(string type, ClaimsPrincipal claimsPrincipal, out Claim claim)
        {
            claim = claimsPrincipal.Claims.SingleOrDefault(c => c.Type == type);

            return claim != null;
        }

        private ClaimsPrincipal GetClaimsFromAccessToken(AccessToken accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetAppSettingsSection().GetValue<string>("Secret"));
            var claims = tokenHandler.ValidateToken(accessToken.Token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out var securityToken);

            return claims == null || securityToken == null ? null : claims;
        }
    }
}
