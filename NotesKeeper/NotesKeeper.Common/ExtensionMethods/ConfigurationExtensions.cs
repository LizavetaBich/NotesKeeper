using Microsoft.Extensions.Configuration;

namespace NotesKeeper.Common.ExtensionMethods
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationSection GetAppSettingsSection(this IConfiguration configuration)
        {
            return configuration?.GetSection("AppSettings");
        }
    }
}
