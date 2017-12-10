using System.Linq;
using AngularExample.EF;
using Microsoft.AspNetCore.Mvc;
using PagiNET;
using PagiNET.Pager;
using PagiNET.Paginate;
using PagiNET.Queryable;

namespace AngularExample.Controllers
{
    public class AllCustomersQuery : IQueryWithPagination
    {
        public string OrderBy { get; set; }
        public bool Descending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class CustomersController : Controller
    {
        private readonly Context _context;

        public CustomersController(Context context)
        {
            _context = context;
        }

        [HttpGet, Route("api/customers")]
        public ItemsWithPagination<Customer> GetAllCompanies(AllCustomersQuery query) =>
            AllCustomersQuery
                .SortAndPaginate(query)
                .ToList()
                .ToItemsWithPagination(query);

        private IQueryable<Customer> AllCustomersQuery => _context.Customers.AsQueryable();

        [HttpGet, Route("api/customers/pagination")]
        public PagerModel GetAllCompaniesPagerModel(AllCustomersQuery pagination) => AllCustomersQuery.GetPagerModel(pagination);
    }

}
