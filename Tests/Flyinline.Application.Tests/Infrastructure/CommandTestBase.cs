using System;
using Flyinline.Persistance.Contexts;
using Northwind.Application.Tests.Infrastructure;

namespace Flyinline.Application.Tests.Infrastructure
{
    public class CommandTestBase : IDisposable
    {
        protected readonly CommonDbContext _commonDbContext;
        protected readonly FlyinlineDbContext _flyinlineDbContext;

        public CommandTestBase()
        {
            _commonDbContext = CommonContextFactory.Create();
            _flyinlineDbContext = FlyinlineContextFactory.Create();

        }

        public void Dispose()
        {
            CommonContextFactory.Destroy(_commonDbContext);
            FlyinlineContextFactory.Destroy(_flyinlineDbContext);
        }
    }
}