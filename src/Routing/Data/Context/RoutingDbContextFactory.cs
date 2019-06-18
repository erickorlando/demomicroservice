using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Data.Context
{
    public class RoutingDbContextFactory : IDesignTimeDbContextFactory<RoutingDbContext>, IDisposable
    {
        public RoutingDbContext CreateDbContext(string[] args)
        {
            var environmentName =
                Environment.GetEnvironmentVariable(
                    "Hosting:Environment");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables()
                .Build();

            var builder = new DbContextOptionsBuilder<RoutingDbContext>();

            var connectionString = configuration.GetConnectionString("default");

            builder.UseSqlServer(connectionString);

            return new RoutingDbContext(builder.Options);
        }

        public void Dispose()
        {
            // Liberar Recursos.
        }
    }
}