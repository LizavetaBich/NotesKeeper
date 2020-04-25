using Microsoft.AspNetCore.Http;
using NotesKeeper.Common.Interfaces.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesKeeper.WebApi.Framework.Helper
{
    public class ConnectionStringExtractor
    {
        private readonly IDbContext _masterContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConnectionStringExtractor(IDbContext master, IHttpContextAccessor contextAccessor)
        {
            _masterContext = master;
            _httpContextAccessor = contextAccessor;
        }

        public string GetUserConnectionString()
        {
            var request = _httpContextAccessor.HttpContext?.Request;

            if (request == null || !request.Query.ContainsKey("userId") || !request.Query.TryGetValue("userId", out var userId))
            {
                return null;
            }

            if (!Guid.TryParse(userId.ToString(), out var id))
            {
                return null;
            }

            var user = _masterContext.Users.SingleOrDefault(x => x.Id == id);

            if (user == null)
            {
                return null;
            }

            return user.DbConnectionString;
        }
    }
}
