using System;
using Flyinline.Persistance.Contexts;
using Flyinline.Persistance.Seeding;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Application.Tests.Infrastructure
{
    public class FlyinlineContextFactory
    {
        public static FlyinlineDbContext Create()
        {
            var options = new DbContextOptionsBuilder<FlyinlineDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new FlyinlineDbContext(options);

            context.Database.EnsureCreated();

            FlyinlineInitializer.Initialize(context);

            context.SaveChanges();

            return context;
        }

        public static void Destroy(FlyinlineDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}