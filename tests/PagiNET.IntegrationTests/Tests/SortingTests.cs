using System;
using System.Linq;
using System.Threading.Tasks;
using ExampleDbContext.Entities;
using Microsoft.EntityFrameworkCore;
using PagiNET.IntegrationTests.Fixtures;
using PagiNET.Paginate;
using PagiNET.Queryable;
using PagiNET.Sort;
using Shouldly;
using Xunit;

namespace PagiNET.IntegrationTests.Tests
{
    public class SortingTests : IClassFixture<RealDatabaseTestFixture>
    {
        private readonly RealDatabaseTestFixture _realDbFixture;

        public SortingTests(RealDatabaseTestFixture realDbFixture)
        {
            _realDbFixture = realDbFixture;
        }

        [Fact]
        public async Task pagination_returns_items_in_correct_order_ascending()
        {
            const int page = 1;
            const int pageSize = 5;

            var sorting = Ascending<Customer, DateTime>.By(c => c.BirthDate);
            var pagination = Pagination.Set(page, pageSize);

            var customers =
                await _realDbFixture.QueryAsync(ctx => ctx.Customers
                    .SortAndTakePage(sorting, pagination)
                    .ToListAsync());

            customers
                .Select(c => c.BirthDate.Ticks)
                .Aggregate(Math.Min)
                .ShouldBe(customers.First().BirthDate.Ticks);

        }

        [Fact]
        public async Task pagination_returns_items_in_correct_order_descending()
        {
            const int page = 1;
            const int pageSize = 5;

            var sorting = Descending<Customer, DateTime>.By(c => c.BirthDate);
            var pagination = Pagination.Set(page, pageSize);

            var customers =
                await _realDbFixture.QueryAsync(ctx =>
                    ctx.Customers
                       .SortAndTakePage(sorting, pagination)
                       .ToListAsync());

            customers
                .Select(c => c.BirthDate.Ticks)
                .Aggregate(Math.Max)
                .ShouldBe(customers.First().BirthDate.Ticks);
        }
    }
}