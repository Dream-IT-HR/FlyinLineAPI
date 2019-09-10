using Flyinline.Application.Interfaces;
using Flyinline.Domain.Entities.Common;
using Flyinline.Domain.Views.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flyinline.Persistance.Contexts
{
    public partial class CommonDbContext : CommonDbGeneratedContext, ICommonDbContext
    {
        public CommonDbContext(): base()
        {

        }

        public CommonDbContext(DbContextOptions<CommonDbGeneratedContext> options): base(options)
        {

        }

        public DbQuery<ClaimPermission> ClaimPermission { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Query<ClaimPermission>(query =>
            {
                query.ToView("ClaimPermission", "Common");
            });
        }

    }
}
