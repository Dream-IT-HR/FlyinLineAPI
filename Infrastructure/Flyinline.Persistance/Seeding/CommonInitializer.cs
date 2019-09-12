using Flyinline.Domain.Entities.Common;
using Flyinline.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flyinline.Persistance.Seeding
{
    public static class CommonInitializer
    {

        public static void Initialize(CommonDbContext commonDbContext)
        {
            SeedPrincipal(commonDbContext);
            SeedRole(commonDbContext);
        }

        private static void SeedPrincipal(CommonDbContext context)
        {
            for (int i = 0; i < 10; i++)
            {
                var nRes = i % SeedHelpers.Fullnames.Count;
                var n = (int)(i / (decimal)SeedHelpers.Fullnames.Count);

                string email = SeedHelpers.GetEmailFromFullName(SeedHelpers.Fullnames[nRes] + n.ToString());

                context.Principal.Add(
                    new Principal { Id = SeedHelpers.Guids[i], Username = email, SuperAdmin = (i == 0 ? true : false) }
                );
            }
        }

        private static void SeedRole(CommonDbContext context)
        {
            context.Role.Add(new Role() { Id = SeedHelpers.Guids[11], Name = "Administrator" });
            context.Role.Add(new Role() { Id = SeedHelpers.Guids[12], Name = "Client" });
            context.Role.Add(new Role() { Id = SeedHelpers.Guids[13], Name = "BusinessOwner" });
        }
    }
}
