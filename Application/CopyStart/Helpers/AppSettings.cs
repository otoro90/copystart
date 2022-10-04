using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace CopyStart.Helpers
{

    public class AppSettings
    {

        static private IConfiguration _configuration = null;

        static public IConfiguration GetConfiguration()
        {
            if (_configuration == null)
            {

                string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                if (String.IsNullOrWhiteSpace(environment))
                    environment = "Development";
                // throw new ArgumentNullException ("Environment not found in ASPNETCORE_ENVIRONMENT");

                // Set up configuration sources.
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

                _configuration = builder.Build();
            }

            return _configuration;
        }
    }

}