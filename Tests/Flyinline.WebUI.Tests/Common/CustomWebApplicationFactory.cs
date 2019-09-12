using System;
using Flyinline.Application.Interfaces;
using Flyinline.Persistance.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
                // Add a database context using an in-memory 
                // database for testing.
                services.AddDbContext<IFlyinlineDbContext, FlyinlineDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting",root );
                    options.UseInternalServiceProvider(serviceProvider);
                });

                services.AddDbContext<ICommonDbContext, CommonDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting", root);
                    options.UseInternalServiceProvider(serviceProvider);
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context (NorthwindDbContext)
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var commonContext = scopedServices.GetRequiredService<ICommonDbContext>();
                    var flyinlineContext = scopedServices.GetRequiredService<IFlyinlineDbContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    var concreteCommonContext = (CommonDbContext)commonContext;
                    var concreteFlyinlineContext = (FlyinlineDbContext)flyinlineContext;

                    // Ensure the database is created.
                    concreteCommonContext.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with test data.
                        Utilities.InitializeDbForTests(concreteCommonContext, concreteFlyinlineContext);
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
