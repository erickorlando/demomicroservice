using Microsoft.Extensions.Configuration;
using Provider;
using System;
using System.IO;

namespace Repository.Factory
{
    public static class SqlServiceFactory
    {
        public static SqlServerService CreateService()
        {
            var environmentName =
                Environment.GetEnvironmentVariable(
                    "Hosting:Environment");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Configuration/swagger.json", optional: false, reloadOnChange: true)
                .AddJsonFile("Configuration/appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"Configuration/appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            return new SqlServerService(configuration.GetConnectionString("default"));
        }
    }
}