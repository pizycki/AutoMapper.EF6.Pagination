using System.Linq;
using PagiNET.IntegrationTests.Fixtures;
using Shouldly;
using Xunit;

namespace PagiNET.IntegrationTests.Tests
{
    public class Lab : IClassFixture<RealDatabaseTestFixture>
    {
        private readonly RealDatabaseTestFixture _fixture;

        public Lab(RealDatabaseTestFixture fixture) => _fixture = fixture;

        [Theory, InlineData(5, 10)]
        public void can_take_page_without_sorting_with_ef(int skip, int take) =>
            _fixture.Query(ctx =>
                ctx.Customers.Skip(skip).Take(take).ToList()).Count.ShouldBe(take);

        [Fact]
        public void can_sort_without_paging() =>
            _fixture.Query(ctx =>
                ctx.Customers.OrderBy(c => c.Id));
    }
}
