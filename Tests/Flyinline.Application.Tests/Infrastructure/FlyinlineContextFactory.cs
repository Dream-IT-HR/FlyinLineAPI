using System;
using Flyinline.Application.Tests.Helpers;
using Flyinline.Domain.Entities.Flyinline;
using Flyinline.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Application.Tests.Infrastructure
{
    public class FlyinlineContextFactory
    {
        public static FlyinlineDbContext Create()
        {
            var options = new DbContextOptionsBuilder<FlyinlineDbGeneratedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new FlyinlineDbContext(options);

            context.Database.EnsureCreated();

            for (int i = 0; i < 10; i++)
            {
                var nRes = i % SeedHelpers.Fullnames.Count;
                var n = (int)(i / (decimal)SeedHelpers.Fullnames.Count);

                string email = SeedHelpers.GetEmailFromFullName(SeedHelpers.Fullnames[nRes] + n.ToString());

                context.UserDetail.Add(
                    new UserDetail { Id = SeedHelpers.Guids[i], Fullname = SeedHelpers.Fullnames[nRes], Email = email, Username = email }
                );
            }
            
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