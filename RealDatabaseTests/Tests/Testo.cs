using System;
using System.Linq;
using AutoMapper.EF6.Pagination;
using AutoMapper.EF6.Pagination.Models;
using NUnit.Framework;
using RealDatabaseTests.Setup;
using SampleDatabase.Context;
using SampleDatabase.Context.Entities;
using Shouldly;

namespace RealDatabaseTests.Tests
{
    [TestFixture]
    public class PaginationTests
    {
        private TestDatabaseManager DbManager { get; } = TestDatabaseManager.Create();

        [OneTimeSetUp]
        public void Setup() => DbManager.CreateDatabaseFromScratch();

        [OneTimeTearDown]
        public void Teardown() => DbManager.DropDatabase();

        [Test]
        public void pagination_returns_correct_number_of_items()
        {
            const int pageSize = 20;

            using (var ctx = new CompanyDbContext())
            {
                var companies = ctx.Companies
                    .SortAndPaginate(Ascending<Company, int>.By(x => x.Id), Pagination.Set(1, pageSize))
                    .ToList();

                companies.Count.ShouldBe(pageSize);
            }
        }
    }
}
