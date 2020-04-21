using Microsoft.Extensions.Configuration;

namespace WrpCcNocWeb.Helpers
{
    public static class GetEmailConfiguration
    {
        public static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder()
                         .SetBasePath(System.AppContext.BaseDirectory)
                         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}
