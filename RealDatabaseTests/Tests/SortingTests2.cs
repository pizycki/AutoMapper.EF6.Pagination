using System.Linq;
using AutoMapper.EF6.Pagination;
using AutoMapper.EF6.Pagination.Extensions;
using AutoMapper.EF6.Pagination.Models;
using NUnit.Framework;
using RealDatabaseTests.Setup;
using SampleDatabase.Context;
using SampleDatabase.Context.Entities;
using Shouldly;

namespace RealDatabaseTests.Tests
{
    [TestFixture]
    public class SortingTests2
    {
        private TestDatabaseManager DbManager { get; } = TestDatabaseManager.Create();

        [OneTimeSetUp]
        public void Setup() => DbManager.CreateDatabaseFromScratch();

        [OneTimeTearDown]
        public void Teardown() => DbManager.DropDatabase();

        [Test]
        public void should_not_fail()
        {
            const int page = 1;
            const int pageSize = 5;
            const string columnName = "OwnerId";

            var sorting = Ascending<Company>.By(columnName);
            var pagination = Pagination.Set(page, pageSize);

            using (var ctx = new CompanyDbContext())
            {
                var companies = ctx.Companies
                    .SortAndPaginate(sorting, pagination)
                    .ToList();

                var fst = companies.First();
                companies.Skip(1).All(c => c.OwnerId > fst.OwnerId).ShouldBeTrue();
            }
        }
    }
}