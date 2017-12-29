using System;
using System.Linq;
using System.Threading.Tasks;
using ExampleDbContext;
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
    public class PaginationTests : IClassFixture<RealDatabaseTestFixture>
    {
        private readonly RealDatabaseTestFixture _realDbFixture;

        public PaginationTests(RealDatabaseTestFixture realDbFixture)
        {
            _realDbFixture = realDbFixture;
        }

        [Fact]
        public async Task pagination_returns_correct_number_of_items()
        {
            const int page = 1;
            const int pageSize = 20;

            var companies =
                await _realDbFixture.QueryAsync(ctx =>
                    ctx.Customers
                        .SortAndTakePage(
                            Ascending<Customer, Guid>.By(x => x.Id),
                            Pagination.Set(page, pageSize))
                        .ToListAsync());

            companies.Count.ShouldBe(pageSize);
        }

        [Fact(Skip = "Waits for generic comparer")]
        public async Task pagination_called_twice_returns_the_same_set()
        {
            // Arrange
            const int page = 1;
            const int pageSize = 20;
            IQueryable<Customer> GetCustomers(Context ctx) =>
                ctx.Customers
                   .SortAndTakePage(Ascending<Customer, Guid>.By(x => x.Id),
                                    Pagination.Set(page, pageSize));

            // Act
            var ls1 = await _realDbFixture.QueryAsync(ctx => GetCustomers(ctx).ToListAsync());
            var ls2 = await _realDbFixture.QueryAsync(ctx => GetCustomers(ctx).ToListAsync());

            // Assert
            // Length equality
            ls1.Count.ShouldBeSameAs(ls2);
        }

        [Fact]
        public async Task running_out_of_pages_range_doesnt_causes_rising_exception()
        {
            var totalItems = await _realDbFixture.QueryAsync(ctx => ctx.Customers.CountAsync());
            const int pageSize = 50;
            var lastPage = totalItems / pageSize;

            var companies = await _realDbFixture.QueryAsync(ctx =>
                ctx.Customers
                   .SortAndTakePage(
                       Ascending<Customer, Guid>.By(x => x.Id),
                       Pagination.Set(lastPage + 2, pageSize))
                   .ToListAsync());

            companies.ShouldNotBeNull();
            companies.ShouldBeEmpty();
        }

        [Fact]
        public async Task paginating_empty_set_returns_empty_list()
        {
            const int pageSize = 20;
            const int page = 1;

            var companies =
                await _realDbFixture.QueryAsync(ctx =>
                    ctx.Customers
                        .Where(x => x.Id == Guid.Empty) // will always return no entities
                        .SortAndTakePage(
                            Ascending<Customer, Guid>.By(x => x.Id),
                            Pagination.Set(page, pageSize))
                        .ToListAsync());

            companies.ShouldNotBeNull();
            companies.ShouldBeEmpty();
        }
    }
}
