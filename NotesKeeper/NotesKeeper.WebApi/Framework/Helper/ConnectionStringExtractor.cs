using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NotesKeeper.BusinessLayer;
using NotesKeeper.Common.Interfaces.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesKeeper.WebApi.Framework.Helper
{
    public class ConnectionStringExtractor
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConnectionStringExtractor(ITokenService tokenService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _tokenService = tokenService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserConnectionString()
        {
            var userDbConnectionStringPattern = _configuration.GetConnectionString("UserConnectionPattern");

            try
            {
                var userId = _tokenService.GetUserIdFromClaimsPrincipal(_httpContextAccessor.HttpContext?.User);

                return string.Format(userDbConnectionStringPattern, userId);
            } catch
            {
                return null;
            }
        }
    }
}
