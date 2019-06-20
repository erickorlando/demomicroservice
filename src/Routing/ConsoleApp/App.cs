using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Utility;

namespace ConsoleApp
{
    public class App
    {
        private readonly ILogger<App> _logger;
        private readonly Appsettings _appSettings;

        public App(IOptions<Appsettings> appSettings, ILogger<App> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
        }

        public async Task Run()
        {
            await Task.CompletedTask;
        }
    }
}