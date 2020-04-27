using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NotesKeeper.Common.ExtensionMethods;
using NotesKeeper.Common.Interfaces.BusinessLayer;
using NotesKeeper.Common.Interfaces.DataAccess;
using NotesKeeper.Common.Models;
using NotesKeeper.Common.Models.AccountModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NotesKeeper.BusinessLayer
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IDbContext _dbContext;

        public AccountService(IMapper mapper, IConfiguration configuration, IDbContext dbContext)
        {
            _mapper = mapper;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public async Task<ApplicationUser> LoginUser(LoginModel loginModel)
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Email == loginModel.Email && x.Password == loginModel.Password);

            if (user ==null)
            {
                return await Task.FromResult<ApplicationUser>(null).ConfigureAwait(false);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetAppSettingsSection().GetValue<string>("Secret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
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
