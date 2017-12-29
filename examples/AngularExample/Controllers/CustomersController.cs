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
        public string OrderBy { get; set; }
        public bool Descending { get; set; }
        public int Number { get; set; }
        public int Size { get; set; }
        public bool IncludeTotalPages { get; set; } = false;
    }

    public class CustomersController : Controller
    {
        private readonly Context _context;

        public CustomersController(Context context)
        {
            _context = context;
        }

        [HttpGet, Route("api/customers")]
        public Page<Customer> GetCustomersPage(AllCustomersQuery query) =>
            query.IncludeTotalPages
            ? QueryForCustomersPage(query)
            : query.GetPageAndTotalPages(
                getPage: q => QueryForCustomersPage(q),
                getTotalPages: q => AllCustomersQuery.CountPages(q));

        private Page<Customer> QueryForCustomersPage(IQueryWithPage query) =>
            AllCustomersQuery
                .SortAndTakePage(query)
                .ToList()
                .AsPage(query);

        private IQueryable<Customer> AllCustomersQuery => _context.Customers.AsQueryable();
    }
}