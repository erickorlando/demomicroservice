using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var services = new ServiceCollection();
                ConfigureServices(services);

                var serviceProvider = services.BuildServiceProvider();

                await serviceProvider.GetService<App>().Run();

                await TestApiRestFromClient(serviceProvider);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} {ex.InnerException?.Message}");
            }
            finally
            {
                Console.ReadLine();
            }

        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder =>
                builder
                    .AddDebug()
                    .AddConsole()
                );

            // build config
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();

            services.AddOptions();
            services.Configure<AppSettings>(configuration.GetSection("Security"));

            // add app
            services.AddTransient<App>();
            services.AddTransient<TestFromApi>();
        }

        private static async Task TestApiRestFromClient(ServiceProvider provider)
        {
            var test = provider.GetService<TestFromApi>();
            await test.Execute();
        }
    }
}
