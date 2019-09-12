using System;
using Flyinline.Domain.Entities.Common;
using Flyinline.Persistance.Contexts;
using Flyinline.Persistance.Seeding;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Application.Tests.Infrastructure
{
    public class CommonContextFactory
    {
        public static CommonDbContext Create()
        {
            var options = new DbContextOptionsBuilder<CommonDbGeneratedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new CommonDbContext(options);

            context.Database.EnsureCreated();

            CommonInitializer.Initialize(context);
            
            context.SaveChanges();

            return context;
        }

        public static void Destroy(CommonDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}