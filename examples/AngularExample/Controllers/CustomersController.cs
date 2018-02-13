using System.Linq;
using ExampleDbContext;
using ExampleDbContext.Entities;
using Microsoft.AspNetCore.Mvc;
using PagiNET.Pager;
using PagiNET.Paginate;
using PagiNET.Queryable;

namespace AngularExample.Controllers
{
    public class AllCustomersQuery : IQueryWithPage
    {
        // Sorting
        public string OrderBy { get; set; }
        public bool Descending { get; set; }

        // Pagination
        public int Number { get; set; }
        public int Size { get; set; }
        public bool IncludeTotalPages { get; set; } = false;
    }

    public class CustomersController : Controller
    {
        private readonly Context _context;

        public CustomersController(Context context) => _context = context;

        [HttpGet, Route("api/customers/sorted")]
        public Page<Customer> GetCustomersSortedPage(AllCustomersQuery query) =>
            query.IncludeTotalPages
            ? QueryForSortedCustomersPage(query)
            : query.GetPageAndTotalPages(
                getPage: QueryForSortedCustomersPage,
                getTotalPages: CountCustomerPages);

        [HttpGet, Route("api/customers")]
        public Page<Customer> GetCustomersPage(AllCustomersQuery query) =>
            query.IncludeTotalPages
                ? QueryForCustomersPage(query)
                : query.GetPageAndTotalPages(
                    getPage: QueryForSortedCustomersPage,
                    getTotalPages: CountCustomerPages);

        private Page<Customer> QueryForSortedCustomersPage(IQueryWithPage query) =>
            AllCustomersQuery
                .SortAndTakePage(query)
                .ToList()
                .AsPage(query);

        private Page<Customer> QueryForCustomersPage(IQueryWithPage query) =>
            AllCustomersQuery
                .TakePage(query)
                .ToList()
                .AsPage(query);

        private int CountCustomerPages(IQueryWithPage query) =>
            AllCustomersQuery.CountPages(query);

        private IQueryable<Customer> AllCustomersQuery =>
            _context.Customers.AsQueryable();
    }
}