using System;
using System.Threading.Tasks;
using ExampleDbContext.Entities;
using NUnit.Framework;
using PagiNET.IntegrationTests.EFCore.Setup;
using PagiNET.Paginate;
using PagiNET.Sort;

namespace PagiNET.IntegrationTests.EFCore.Tests
{
    [TestFixture]
    public class SortingTests2
    {
        private ITestDatabaseManager DbManager { get; } = TestDatabaseManager.Create();

        [OneTimeSetUp]
        public void Setup() => DbManager.CreateDatabase();

        [OneTimeTearDown]
        public void Teardown() => DbManager.DropDatabase();

        [Test]
        public async Task should_not_fail()
        {
            const int page = 1;
            const int pageSize = 5;
            const string columnName = "OwnerId";

            var sorting = Ascending<Customer>.By(columnName);
            var pagination = Pagination.Set(page, pageSize);

            using (var ctx = CreateDbContext())
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
}