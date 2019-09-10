using Flyinline.Application.Interfaces;
using Flyinline.Domain.Entities.Flyinline;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flyinline.Persistance.Contexts
{
    public partial class FlyinlineDbContext : FlyinlineDbGeneratedContext, IFlyinlineDbContext
    {
        public FlyinlineDbContext(): base()
        {
        }

        public FlyinlineDbContext(DbContextOptions<FlyinlineDbGeneratedContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
