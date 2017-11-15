using System.Collections.Generic;
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
            const int page = 1;
            const int pageSize = 20;

            using (var ctx = new CompanyDbContext())
            {
                var companies = ctx.Companies
                                   .SortAndPaginate(
                                        Ascending<Company, int>.By(x => x.Id),
                                        Pagination.Set(page, pageSize))
                                   .ToList();

                companies.Count.ShouldBe(pageSize);
            }
        }


        [Ignore("Waits for generic comparer")]
        [Test]
        public void pagination_called_twice_returns_the_same_set()
        {
            const int page = 1;
            const int pageSize = 20;
            List<Company> ls1;
            List<Company> ls2;

            using (var ctx = new CompanyDbContext())
                ls1 = ctx.Companies
                         .SortAndPaginate(Ascending<Company, int>.By(x => x.Id), Pagination.Set(page, pageSize))
                         .ToList();

            using (var ctx = new CompanyDbContext())
                ls2 = ctx.Companies
                         .SortAndPaginate(Ascending<Company, int>.By(x => x.Id), Pagination.Set(page, pageSize))
                         .ToList();

            // Length equality
            ls1.Count.ShouldBeSameAs(ls2);
        }

        [Test]
        public void running_out_of_pages_range_doesnt_causes_rising_exception()
        {
            const int pageSize = 50;
            var totalItems = DbManager.Feeder.CompaniesCount;
            var lastPage = totalItems / pageSize;

            using (var ctx = new CompanyDbContext())
            {
                var companies = ctx.Companies
                                   .SortAndPaginate(
                                        Ascending<Company, int>.By(x => x.Id),
                                        Pagination.Set(lastPage + 1, pageSize))
                                   .ToList();

                companies.ShouldNotBeNull();
                companies.ShouldBeEmpty();
            }
        }

        [Test]
        public void paginating_empty_set_returns_empty_list()
        {
            const int pageSize = 20;
            const int page = 1;

            using (var ctx = new CompanyDbContext())
            {
                var companies = ctx.Companies
                                   .Where(x => x.Id < 0) // Entity cannot have negative ID, so it will always return no entities
                                   .SortAndPaginate(
                                       Ascending<Company, int>.By(x => x.Id),
                                       Pagination.Set(page, pageSize))
                                   .ToList();

                companies.ShouldNotBeNull();
                companies.ShouldBeEmpty();
            }
        }

    }

}
