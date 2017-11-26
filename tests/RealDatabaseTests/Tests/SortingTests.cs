using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NUnit.Framework;
using PagiNET.Extensions;
using PagiNET.Models;
using RealDatabaseTests.Setup;
using SampleDatabase.Context;
using SampleDatabase.Context.Entities;
using Shouldly;

namespace RealDatabaseTests.Tests
{
    [TestFixture]
    public class SortingTests
    {
        private TestDatabaseManager DbManager { get; } = TestDatabaseManager.Create();

        [OneTimeSetUp]
        public void Setup() => DbManager.CreateDatabaseFromScratch();

        [OneTimeTearDown]
        public void Teardown() => DbManager.DropDatabase();

        [Test]
        public void pagination_returns_items_in_correct_order_ascending()
        {
            const int page = 1;
            const int pageSize = 5;

            var sorting = Ascending<Company, int>.By(c => c.OwnerId);
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

        [Test]
        public void pagination_returns_items_in_correct_order_descending()
        {
            const int page = 1;
            const int pageSize = 5;

            var sorting = Descending<Company, int>.By(c => c.OwnerId);
            var pagination = Pagination.Set(page, pageSize);

            using (var ctx = new CompanyDbContext())
            {
                var companies = ctx.Companies
                    .SortAndPaginate(sorting, pagination)
                    .ToList();

                var fst = companies.First();
                companies.Skip(1).All(c => c.OwnerId < fst.OwnerId).ShouldBeTrue();

            }
        }
    }
}