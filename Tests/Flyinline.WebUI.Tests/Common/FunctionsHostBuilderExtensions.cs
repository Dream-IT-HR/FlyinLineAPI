using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flyinline.WebUI.Tests.Common
{
    public static class FunctionsHostBuilderExtensions
    {
        /// <summary>
        /// https://docs.microsoft.com/en-us/azure/azure-functions/functions-app-settings
        ///local.settings.json is part of environment variables and will overwrite anything in appsettings. It doesn't support nested json
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IConfiguration GetConfigurationWithAppSettings(IServiceCollection services)
        {
            string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            var configurationBuilder = new ConfigurationBuilder();

            var descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(IConfiguration));
            if (descriptor?.ImplementationInstance is IConfiguration configRoot)
            {
                configurationBuilder.AddConfiguration(configRoot);
            }

            var configuration = configurationBuilder.SetBasePath(currentDirectory)
                .AddEnvironmentVariables();

            return configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false).Build();
        }
    }
}
