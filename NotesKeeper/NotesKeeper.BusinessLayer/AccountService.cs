using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NotesKeeper.Common;
using NotesKeeper.Common.ExtensionMethods;
using NotesKeeper.Common.Interfaces.BusinessLayer;
using NotesKeeper.Common.Interfaces.DataAccess;
using NotesKeeper.Common.Models;
using NotesKeeper.Common.Models.AccountModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NotesKeeper.BusinessLayer
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IDbContext _dbContext;
        private readonly ITokenService _tokenService;

        public AccountService(IMapper mapper, IConfiguration configuration, IDbContext dbContext, ITokenService tokenService)
        {
            _mapper = mapper;
            _configuration = configuration;
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        public async Task<(ApplicationUser User,RefreshToken RefreshToken)> LoginUser(LoginModel loginModel)
        {
            var user = _dbContext.Users
                .Include(x => x.RefreshTokens)
                .SingleOrDefault(x => x.Email == loginModel.Email && x.Password == loginModel.Password);

            if (user ==null)
            {
                return await Task.FromResult<(ApplicationUser, RefreshToken)>((null, null)).ConfigureAwait(false);
            }

            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshTokens.Add(refreshToken);
            await _dbContext.SaveChangesAsync(true);

            user.Token = _tokenService.GenerateAccessToken(user);

            return (User: user, RefreshToken: refreshToken);
        }

        public async Task LogoutUser(RefreshAccessTokenModel model)
        {
            Guard.IsNotNull(model);

            var userId = _tokenService.GetUserIdFromAccessToken(model.AccessToken);
            var user = _dbContext
                .Users.SingleOrDefault(x => x.Id == userId);

            if (user != null)
            {
                var refreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == model.RefreshToken.Token);

                if (refreshToken != null)
                {
                    //user.RefreshTokens.Remove(refreshToken);
                    _dbContext.RefreshTokens.Remove(refreshToken);
                    await _dbContext.SaveChangesAsync(true);
                }
            }
        }

        public async Task<RefreshAccessTokenModel> RefreshAccessToken(RefreshAccessTokenModel model)
        {
            Guard.IsNotNull(model);

            var isAccessTokenValid = _tokenService.ValidateAccessToken(model.AccessToken);
            var userId = _tokenService.GetUserIdFromAccessToken(model.AccessToken);
            var isRefreshTokenValid = _tokenService.ValidateRefreshToken(model.RefreshToken, userId);
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == userId);

            if (!(isAccessTokenValid && isRefreshTokenValid) || user == null)
            {
                return null;
            }

            model.AccessToken = _tokenService.GenerateAccessToken(user);
            model.RefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshTokens.Add(model.RefreshToken);
            await _dbContext.SaveChangesAsync(true);

            return model;
        }

        public async Task<ApplicationUser> RegisterUser(RegisterModel registerModel)
        {
            if (registerModel.ConfirmPassword != registerModel.Password)
            {
                return await Task.FromResult<ApplicationUser>(null).ConfigureAwait(false);
            }

            if (_dbContext.Users.Any(x => x.Email == registerModel.Email))
            {
                throw new ArgumentException("The user with the same email already exists.");
            }

            var user = _mapper.Map<ApplicationUser>(registerModel);
            user.Id = Guid.NewGuid();

            user.DbConnectionString = string.Format(_configuration.GetConnectionString("UserConnectionPattern"), user.Id);

            await _dbContext.Users.AddAsync(user).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync(true);

            return user;
        }
    }
}
