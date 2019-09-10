using System;
using Flyinline.Persistance.Contexts;
using Xunit;

namespace Northwind.Application.Tests.Infrastructure
{
    public class QueryTestFixture : IDisposable
    {
        public CommonDbContext CommonDbContext { get; private set; }
        public FlyinlineDbContext FlyinlineDbContext { get; private set; }

        public QueryTestFixture()
        {
            CommonDbContext = CommonContextFactory.Create();
            FlyinlineDbContext = FlyinlineContextFactory.Create();
        }

        public void Dispose()
        {
            CommonContextFactory.Destroy(CommonDbContext);
            FlyinlineContextFactory.Destroy(FlyinlineDbContext);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}