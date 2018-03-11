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
            const string columnName = "BirthDate";

            var sorting = Ascending<Person>.By(columnName);
            var pagination = Pagination.Set(page, pageSize);

            var customers = await _realDbFixture.QueryAsync(
                            ctx => ctx.Persons
                                      .TakeSortedPage(sorting, pagination)
                                      .ToListAsync());

            customers
                .Select(c => c.BirthDate.Ticks)
                .Aggregate(Math.Min)
                .ShouldBe(customers.First().BirthDate.Ticks);
        }
    }
}