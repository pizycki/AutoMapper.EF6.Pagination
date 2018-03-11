using System.Linq;
using ExampleDbContext;
using ExampleDbContext.Entities;
using Microsoft.AspNetCore.Mvc;
using PagiNET;
using PagiNET.Pager;
using PagiNET.Paginate;
using PagiNET.Queryable;
using PagiNET.Sort;

/**
 * Employers fixed page with sorting
 */

namespace AngularExample.Controllers
{
    public class EmployersController : Controller
    {
        private readonly Context _context;

        public EmployersController(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Queries context for requested page of employers.
        /// Customers are sorted in default manner.
        /// </summary>
        /// <param name="queryParams">Quering parameters.</param>
        /// <returns>Requested page of customers.</returns>
        [HttpGet, Route("api/employers")]
        public Page<Employer> GetEmployersPage(GetEmployersPageQueryParams queryParams)
        {
            return queryParams.TotalPages
                ? queryParams.GetSortedPageAndPagesTotal(page: QueryForEmployersPage, pagesTotal: CountPages)
                : QueryForEmployersPage(queryParams);

            Page<Employer> QueryForEmployersPage(IPageAndSortInfo info) =>
                GetEmployersQueryable().SingleSortedPage(Sorting<Employer>.By(info.OrderBy, info.Descending), Pagination.Set(info.Number, info.Size));
            IQueryable<Employer> GetEmployersQueryable() => _context.Employers.AsQueryable();
            int CountPages(IPageInfo query) => GetEmployersQueryable().CountPages(query);
        }
    }

    public class GetEmployersPageQueryParams : IPageAndSortInfo
    {
        public int Number { get; set; } = 1;
        public int Size { get; set; } = 20;

        public string OrderBy { get; set; }
        public bool Descending { get; set; }
        
        public bool TotalPages { get; set; }
    }
}