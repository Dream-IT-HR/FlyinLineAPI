using System;
using System.Configuration;
using System.IO;
using Flyinline.Application.Interfaces;
using Flyinline.Persistance.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace Flyinline.WebUI.Tests.Common
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                var root = new InMemoryDatabaseRoot();

                //services.AddDbContext<IFlyinlineDbContext, FlyinlineDbContext>(options =>
                //{
                //    options.UseInMemoryDatabase("InMemoryDbForTesting", root);
                //    options.UseInternalServiceProvider(serviceProvider);
                //});

                //add settings file to config providers:
                var _configuration = FunctionsHostBuilderExtensions.GetConfigurationWithAppSettings(services);
                // builder.Services.Replace(ServiceDescriptor.Singleton(typeof(IConfiguration), _configuration));
                // _credentialSettings = _configuration.GetSection("Values:Credentials").Get<CredentialSettings>();



                services.AddDbContext<IFlyinlineDbContext, FlyinlineDbContext>(options =>
                {
                    options.UseSqlServer(_configuration.GetConnectionString("flyinline_db"));
                });


                // Build the service provider.
                var sp = services.BuildServiceProvider();
                // Create a scope to obtain a reference to the database
                // context (NorthwindDbContext)
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<IFlyinlineDbContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
                    

                    var concreteContext= (FlyinlineDbContext)context;

                    // Ensure the database is created.
                    concreteContext.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with test data.
                        Utilities.InitializeDbForTests(concreteContext);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"An error occurred seeding the " +
                                            "database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }
    }
}
