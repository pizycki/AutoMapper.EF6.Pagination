using System.Linq;
using ExampleDbContext;
using ExampleDbContext.Entities;
using Microsoft.EntityFrameworkCore;
using PagiNET.IntegrationTests.Fixtures;
using Shouldly;
using Xunit;

namespace PagiNET.IntegrationTests.Tests.Env
{
    /// <summary>
    /// Those tests are supposed to check if deployed sample database is in expected state.
    /// </summary>
    public class DeployedDatabaseTests : IClassFixture<RealDatabaseTestFixture>
    {
        private readonly RealDatabaseTestFixture _realDbFixture;

        public DeployedDatabaseTests(RealDatabaseTestFixture realDbFixture) => _realDbFixture = realDbFixture;

        [Fact]
        public void database_is_not_empty() =>
            _realDbFixture
                .Query(ctx => ctx.Persons.Any())
                .ShouldBeTrue();

        [Fact]
        public void there_are_correct_number_of_people()
        {
            var expected = PeopleSeeder.SampleCustomersList.Count;
            _realDbFixture
                .Query(ctx => ctx.Persons.Count())
                .ShouldBe(expected);
        }

        [Theory]
        [InlineData(Gender.Male)]
        [InlineData(Gender.Female)]
        public void not_all_customers_are_of_one_gender(Gender gender) =>
            _realDbFixture.Query(ctx => ctx.Persons.All(c => c.Gender == gender));
    }
}
