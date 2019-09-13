using System;
using Flyinline.Persistance.Contexts;
using Northwind.Application.Tests.Infrastructure;

namespace Flyinline.Application.Tests.Infrastructure
{
    public class CommandTestBase : IDisposable
    {
        protected readonly FlyinlineDbContext _context;

        public CommandTestBase()
        {
            _context = FlyinlineContextFactory.Create();

        }

        public void Dispose()
        {
            FlyinlineContextFactory.Destroy(_context);
        }
    }
}
