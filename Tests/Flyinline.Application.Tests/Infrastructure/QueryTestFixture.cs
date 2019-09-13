using System;
using Flyinline.Persistance.Contexts;
using Xunit;

namespace Northwind.Application.Tests.Infrastructure
{
    public class QueryTestFixture : IDisposable
    {
        public FlyinlineDbContext FlyinlineDbContext { get; private set; }

        public QueryTestFixture()
        {
            FlyinlineDbContext = FlyinlineContextFactory.Create();
        }

        public void Dispose()
        {
            FlyinlineContextFactory.Destroy(FlyinlineDbContext);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}