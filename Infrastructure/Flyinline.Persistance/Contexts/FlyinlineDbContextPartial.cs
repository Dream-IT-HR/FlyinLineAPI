using Flyinline.Application.Interfaces;
using Flyinline.Domain.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flyinline.Persistance.Contexts
{
    public partial class FlyinlineDbContext : IFlyinlineDbContext
    {
        public DbQuery<ClaimPermission> ClaimPermission { get; set; }

        
        private void AdditionalModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Query<ClaimPermission>(query =>
            {
                query.ToView("ClaimPermission", "Common");
            });
        }
    }
}
