using System.Linq;
using ExampleDbContext;
using ExampleDbContext.Entities;
using Microsoft.AspNetCore.Mvc;
using PagiNET;
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
    }

    public class CustomersController : Controller
    {
        private readonly Context _context;

        public CustomersController(Context context)
        {
            _context = context;
        }

        [HttpGet, Route("api/customers")]
        public Page<Customer> GetAllCompanies(AllCustomersQuery query) =>
            AllCustomersQuery
                .SortAndPaginate(query)
                .ToList()
                .ToItemsWithPagination(query);

        private IQueryable<Customer> AllCustomersQuery => _context.Customers.AsQueryable();


        [HttpGet, Route("api/paginated/customers")]
        public PageAndPager<Customer> GetAllCompaniesWithPager(AllCustomersQuery query) =>
            query.GetPaginatedResult(
                getItems: q => AllCustomersQuery
                    .SortAndPaginate(q)
                    .ToList()
                    .ToItemsWithPagination(q),
                getPager: q => AllCustomersQuery.GetPagerModel(q));

        [HttpGet, Route("api/customers/pagination")]
        public PagerModel GetAllCompaniesPagerModel(AllCustomersQuery pagination) => AllCustomersQuery.GetPagerModel(pagination);
    }

}
