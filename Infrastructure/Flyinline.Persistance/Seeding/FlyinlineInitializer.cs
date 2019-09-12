using Flyinline.Domain.Entities.Flyinline;
using Flyinline.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flyinline.Persistance.Seeding
{
    public static class FlyinlineInitializer
    {

        
        public static void Initialize(FlyinlineDbContext flyinlineDbContext)
        {
            SeedUserDetails(flyinlineDbContext);
        }

        private static void SeedUserDetails(FlyinlineDbContext context)
        {
            for (int i = 0; i < 10; i++)
            {
                var nRes = i % SeedHelpers.Fullnames.Count;
                var n = (int)(i / (decimal)SeedHelpers.Fullnames.Count);

                string email = SeedHelpers.GetEmailFromFullName(SeedHelpers.Fullnames[nRes] + n.ToString());

                context.UserDetail.Add(
                    new UserDetail { Id = SeedHelpers.Guids[i], Fullname = SeedHelpers.Fullnames[nRes], Email = email, Username = email }
                );
            }
        }
    }
}
