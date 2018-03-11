using System.Linq;
using ExampleDbContext;
using ExampleDbContext.Entities;
using Microsoft.AspNetCore.Mvc;
using PagiNET.Pager;
using PagiNET.Paginate;
using PagiNET.Queryables;

/**
 * Customers fixed page
 *
 * The simplest form of pagination.
 * Fixed page size, no sorting, page refreshed after clicking on another page.
 */

namespace AngularExample.Controllers
{
    public class CustomersController : Controller
    {
        private readonly Context _context;

        public CustomersController(Context context)
        {
            _context = context;
        } 

        /// <summary>
        /// Queries context for requested page of customers.
        /// Customers are sorted in default manner.
        /// </summary>
        /// <param name="queryParams">Quering parameters.</param>
        /// <returns>Requested page of customers.</returns>
        [HttpGet, Route("api/customers")]
        public Page<Person> GetCustomersPage(GetCustomersPageQueryParams queryParams)
        {
            return queryParams.GetPageAndTotalPages(page: QueryForCustomersPage, pagesTotal: CountCustomerPages);

            IQueryable<Person> GetCustomersQueryable() => _context.Customers.AsQueryable();
            Page<Person> QueryForCustomersPage(IPageInfo pageInfo) => GetCustomersQueryable().SinglePage(pageInfo);
            int CountCustomerPages(IPageInfo query) => GetCustomersQueryable().CountPages(query);
        }
    }
    
    public class GetCustomersPageQueryParams : IPageInfo
    {
        public int Number { get; set; } = 1;
        public int Size { get; set; } = 20;
    }
}