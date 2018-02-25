using System.Linq;
using ExampleDbContext;
using ExampleDbContext.Entities;
using Microsoft.AspNetCore.Mvc;
using PagiNET.Pager;
using PagiNET.Paginate;
using PagiNET.Queryable;

namespace AngularExample.Controllers
{
    public class CustomersController : Controller
    {
        private readonly Context _context;

        public CustomersController(Context context) => _context = context;

        [HttpGet, Route("api/customers")]
        public Page<Person> GetCustomersPage(AllCustomersQueryParams queryParams) =>
            queryParams.IncludeTotalPages
                ? QueryForCustomersPage(queryParams)
                : queryParams.GetPageAndTotalPages(
                    getPage: QueryForCustomersPage,
                    getTotalPages: CountCustomerPages);

        private Page<Person> QueryForCustomersPage(IQueryWithPage query) =>
            CustomersQueryable
                .TakePage(query)
                .ToList()
                .AsPage(query);

        private int CountCustomerPages(IQueryWithPage query) =>
            CustomersQueryable.CountPages(query);

        private IQueryable<Person> CustomersQueryable => _context.People.AsQueryable();

        public class AllCustomersQueryParams : IQueryWithPage
        {
            // Sorting
            public string OrderBy { get; set; }
            public bool Descending { get; set; }

            // Pagination
            public int Number { get; set; }
            public int Size { get; set; }
            public bool IncludeTotalPages { get; set; } = false;
        }
    }
}