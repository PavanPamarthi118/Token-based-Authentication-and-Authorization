using System.IO;
using Microsoft.Extensions.Configuration;

namespace Token_based_Authentication___Authorization.Helpers
{
    public class ConfigurationHelper
    {
        public static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            return builder.Build();
        }
    }
}
