//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using ExampleDbContext;
//using ExampleDbContext.Entities;
//using Microsoft.EntityFrameworkCore;
//using NUnit.Framework;
//using PagiNET.IntegrationTests.EFCore.Setup;
//using PagiNET.Paginate;
//using PagiNET.Queryable;
//using PagiNET.Sort;
//using Shouldly;

//namespace PagiNET.IntegrationTests.EFCore.Tests
//{
//    [TestFixture]
//    public class PaginationTests
//    {
//        private ITestDatabaseManager DbManager { get; } = new TestDatabaseManager();

//        [OneTimeSetUp]
//        public void Setup() => DbManager.CreateDatabase();

//        [OneTimeTearDown]
//        public void Teardown() => DbManager.DropDatabase();

//        [Test]
//        public async Task pagination_returns_correct_number_of_items()
//        {
//            const int page = 1;
//            const int pageSize = 20;

//            using (var ctx = DbManager.CreateDbContext())
//            {
//                var companies =
//                    await ctx.Customers
//                             .SortAndPaginate(
//                                 Ascending<Customer, Guid>.By(x => x.Id),
//                                 Pagination.Set(page, pageSize))
//                             .ToListAsync();

//                companies.Count.ShouldBe(pageSize);
//            }
//        }


//        [Ignore("Waits for generic comparer")]
//        [Test]
//        public async Task pagination_called_twice_returns_the_same_set()
//        {
//            // Arrange
//            const int page = 1;
//            const int pageSize = 20;
//            List<Customer> ls1;
//            List<Customer> ls2;
//            IQueryable<Customer> GetCustomers(Context ctx) =>
//                ctx.Customers
//                   .SortAndPaginate(Ascending<Customer, Guid>.By(x => x.Id),
//                                    Pagination.Set(page, pageSize));

//            // Act
//            using (var ctx = DbManager.CreateDbContext())
//                ls1 = await GetCustomers(ctx).ToListAsync();

//            using (var ctx = DbManager.CreateDbContext())
//                ls2 = await GetCustomers(ctx).ToListAsync();

//            // Assert
//            // Length equality
//            ls1.Count.ShouldBeSameAs(ls2);
//        }

//        [Test]
//        public async Task running_out_of_pages_range_doesnt_causes_rising_exception()
//        {
//            using (var ctx = DbManager.CreateDbContext())
//            {
//                var totalItems = await ctx.Customers.CountAsync();
//                const int pageSize = 50;
//                var lastPage = totalItems / pageSize;

//                var companies =
//                    await ctx.Customers
//                             .SortAndPaginate(
//                                 Ascending<Customer, Guid>.By(x => x.Id),
//                                 Pagination.Set(lastPage + 1, pageSize))
//                             .ToListAsync();

//                companies.ShouldNotBeNull();
//                companies.ShouldBeEmpty();
//            }
//        }

//        [Test]
//        public async Task paginating_empty_set_returns_empty_list()
//        {
//            const int pageSize = 20;
//            const int page = 1;

//            using (var ctx = DbManager.CreateDbContext())
//            {
//                var companies =
//                    await ctx.Customers
//                        .Where(x => x.Id == Guid.Empty) // will always return no entities
//                        .SortAndPaginate(
//                            Ascending<Customer, Guid>.By(x => x.Id),
//                            Pagination.Set(page, pageSize))
//                        .ToListAsync();

//                companies.ShouldNotBeNull();
//                companies.ShouldBeEmpty();
//            }
//        }
//    }
//}
