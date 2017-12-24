﻿using System.Linq;
using ExampleDbContext;
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

        public DeployedDatabaseTests(RealDatabaseTestFixture realDbFixture)
        {
            _realDbFixture = realDbFixture;
        }

        [Fact]
        public void database_is_not_empty() =>
            _realDbFixture
                .Query(ctx => ctx.Customers.Any())
                .ShouldBeTrue();

        [Fact]
        public void there_are_n_customers()
        {
            var expected = CustomersSeeder.CreateSampleCustomersList().Count;
            _realDbFixture
                .Query(ctx =>
                    ctx.Customers
                       .Select(x => x.Id)
                       .Count())
                .ShouldBe(expected);
        }
    }
}