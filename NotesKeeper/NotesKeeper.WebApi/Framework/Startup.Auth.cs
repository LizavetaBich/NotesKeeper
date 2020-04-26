using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NotesKeeper.Common.ExtensionMethods;
using System.Text;

namespace NotesKeeper.WebApi.Framework
{
    public static class Startup
    {
        public static void AddJWTAuth(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var key = configuration.GetAppSettingsSection().GetValue<string>("Secret");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            serviceCollection.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
