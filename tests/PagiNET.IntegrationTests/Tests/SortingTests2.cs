using System;
using System.Linq;
using System.Threading.Tasks;
using ExampleDbContext;
using ExampleDbContext.Entities;
using Microsoft.EntityFrameworkCore;
using PagiNET.IntegrationTests.Setup;
using PagiNET.Paginate;
using PagiNET.Queryable;
using PagiNET.Sort;
using Shouldly;
using Xunit;

namespace PagiNET.IntegrationTests.Tests
{
    public class SortingTests2 : IClassFixture<RealDatabaseTestFixture>
    {
        private readonly RealDatabaseTestFixture _realDbFixture;

        public SortingTests2(RealDatabaseTestFixture realDbFixture)
        {
            _realDbFixture = realDbFixture;
        }

        [Fact]
        public async Task should_not_fail()
        {
            const int page = 1;
            const int pageSize = 5;
            const string columnName = "OwnerId";

            var sorting = Ascending<Customer>.By(columnName);
            var pagination = Pagination.Set(page, pageSize);

            using (var ctx = _realDbFixture.CreateContext())
            {
                var customers = await ctx.Customers
                    .SortAndPaginate(sorting, pagination)
                    .ToListAsync();

                customers
                    .Select(c => c.BirthDate.Ticks)
                    .Aggregate(Math.Min)
                    .ShouldBe(customers.First().BirthDate.Ticks);
            }
        }
    }

    public class RealDatabaseTestFixture : IDisposable
    {
        private ITestDatabaseManager DbManager { get; } = new TestDatabaseManager();

        public RealDatabaseTestFixture()
        {
            DbManager.CreateDatabase();
        }

        public void Dispose()
        {
            DbManager.DropDatabase();
        }

        public Context CreateContext() => DbManager.CreateDbContext();
    }
}