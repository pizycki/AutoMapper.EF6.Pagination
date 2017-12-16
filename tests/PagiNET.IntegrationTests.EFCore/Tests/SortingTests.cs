using System;
using System.Linq;
using System.Threading.Tasks;
using ExampleDbContext.Entities;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PagiNET.IntegrationTests.EFCore.Setup;
using PagiNET.Paginate;
using PagiNET.Queryable;
using PagiNET.Sort;
using Shouldly;

namespace PagiNET.IntegrationTests.EFCore.Tests
{
    [TestFixture]
    public class SortingTests
    {
        private ITestDatabaseManager DbManager { get; } = TestDatabaseManager.Create();

        [OneTimeSetUp]
        public void Setup() => DbManager.CreateDatabase();

        [OneTimeTearDown]
        public void Teardown() => DbManager.DropDatabase();

        [Test]
        public async Task pagination_returns_items_in_correct_order_ascending()
        {
            const int page = 1;
            const int pageSize = 5;

            var sorting = Ascending<Customer, DateTime>.By(c => c.BirthDate);
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

        [Test]
        public async Task pagination_returns_items_in_correct_order_descending()
        {
            const int page = 1;
            const int pageSize = 5;

            var sorting = Descending<Customer, DateTime>.By(c => c.BirthDate);
            var pagination = Pagination.Set(page, pageSize);

            using (var ctx = CreateDbContext())
            {
                var customers = await ctx.Customers
                    .SortAndPaginate(sorting, pagination)
                    .ToListAsync();

                customers
                    .Select(c => c.BirthDate.Ticks)
                    .Aggregate(Math.Max)
                    .ShouldBe(customers.First().BirthDate.Ticks);
            }
        }
    }
}