using System;
using Flyinline.Application.Tests.Helpers;
using Flyinline.Domain.Entities.Common;
using Flyinline.Persistance.Contexts;
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

            for (int i = 0; i < 10; i++)
            {
                var nRes = i % SeedHelpers.Fullnames.Count;
                var n =  (int) (i / (decimal) SeedHelpers.Fullnames.Count);

                string email = SeedHelpers.GetEmailFromFullName(SeedHelpers.Fullnames[nRes] + n.ToString());

                context.Principal.Add(
                    new Principal { Id = SeedHelpers.Guids[i], Username = email, SuperAdmin = (i == 0 ? true : false) }
                );
            }

            context.Role.Add(new Role() { Id = SeedHelpers.Guids[11], Name = "Administrator" });
            context.Role.Add(new Role() { Id = SeedHelpers.Guids[12], Name = "Client" });
            context.Role.Add(new Role() { Id = SeedHelpers.Guids[13], Name = "BusinessOwner" });

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