using System.Linq;
using ExampleDbContext;
using ExampleDbContext.Entities;
using Microsoft.AspNetCore.Mvc;
using PagiNET.Paginate;
using PagiNET.Queryable;

/**
 * Directors infinite page
 *
 * Page content is loaded automaticly once reaching certain point of page while scrolling down.
 * This paging doesn't involve pager.
 */

namespace AngularExample.Controllers
{
    public class DirectorsController : Controller
    {
        private readonly Context _context;

        public DirectorsController(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Queries context for requested page of directors.
        /// Customers are sorted in default manner.
        /// </summary>
        /// <param name="queryParams">Quering parameters.</param>
        /// <returns>Requested page of customers.</returns>
        [HttpGet, Route("api/directors")]
        public Page<Director> GetDirectorsPage(GetDirectorsPageQueryParams queryParams) => DirectorsQueryable.TakeAsPage(queryParams);

        private IQueryable<Director> DirectorsQueryable => _context.Directors.AsQueryable();
    }

    public class GetDirectorsPageQueryParams : IPageInfo
    {
        public int Number { get; set; } = 1;
        public int Size { get; set; } = 20;
    }
}