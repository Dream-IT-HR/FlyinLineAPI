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
            // var isDevelopmentEnvironment = IsDevelopmentEnvironment();
            //string currentDirectory = IsDevelopmentEnvironment() ?
            //Environment.GetEnvironmentVariable("AzureWebJobsScriptRoot") :
            //$"{Environment.GetEnvironmentVariable("HOME")}\\site\\wwwroot";

            string currentDirectory = System.IO.Directory.GetCurrentDirectory();

            //todo rename if using IOptions in the end
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configurationBuilder = new ConfigurationBuilder();

                       
            var descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(IConfiguration));
            if (descriptor?.ImplementationInstance is IConfiguration configRoot)
            {
                configurationBuilder.AddConfiguration(configRoot);
            }

            var configuration = configurationBuilder.SetBasePath(currentDirectory)
                .AddEnvironmentVariables();



            //if (isDevelopmentEnvironment)
            //{
            //    return configuration
            //     .AddJsonFile("local.settings.json", optional: true, reloadOnChange: false)
            //        .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: false)
            //    .Build();
            //}
            //else
            //{
                return configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false).Build();
            //}
        }



        //public static bool IsDevelopmentEnvironment()
        //{
        //    return "Development".Equals(Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT"), StringComparison.OrdinalIgnoreCase);
        //}
    }
}
