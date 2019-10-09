using Flyinline.Domain.Entities;
using Flyinline.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flyinline.Persistance.Seeding
{
    public static class FlyinlineInitializer
    {
        public static void Initialize(FlyinlineDbContext context)
        {
            SeedPrincipal(context);
            SeedRole(context);
            SeedUserDetails(context);
        }

        private static void SeedPrincipal(FlyinlineDbContext context)
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

        private static void SeedRole(FlyinlineDbContext context)
        {
            context.Role.Add(new Role() { Id = SeedHelpers.Guids[11], Name = "Administrator" });
            context.Role.Add(new Role() { Id = SeedHelpers.Guids[12], Name = "Client" });
            context.Role.Add(new Role() { Id = SeedHelpers.Guids[13], Name = "BusinessOwner" });
        }

        private static void SeedUserDetails(FlyinlineDbContext context)
        {
            for (int i = 0; i < 10; i++)
            {
                var nRes = i % SeedHelpers.Fullnames.Count;
                var n = (int)(i / (decimal)SeedHelpers.Fullnames.Count);

                string email = SeedHelpers.GetEmailFromFullName(SeedHelpers.Fullnames[nRes] + n.ToString());

                context.UserDetail.Add(
                    new UserDetail { Id = SeedHelpers.Guids[i],
                        FirstName = SeedHelpers.GetFirstNameFromFullName( SeedHelpers.Fullnames[nRes]),
                        LastName = SeedHelpers.GetLastNameFromFullName(SeedHelpers.Fullnames[nRes]),
                        Email = email, Username = email }
                );
            }
        }
    }
}
