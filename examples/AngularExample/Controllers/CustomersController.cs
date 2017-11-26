using System;
using System.Linq;
using AngularExample.EF;
using Microsoft.AspNetCore.Mvc;
using PagiNET.Extensions;
using PagiNET.Models;

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

    public class CommonPaginationQuery : IPaginationInfo
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class PagerModel
    {
        public int Pages { get; set; }
    }

    public static class PaginationBla
    {
        public static PagerModel GetPagerModel<T>(this IQueryable<T> query, IPaginationInfo info)
        {
            var total = query.Count();

            return new PagerModel
            {
                // ReSharper disable once PossibleLossOfFraction
                Pages = (int)Math.Ceiling((double)(total / info.PageSize))
            };
        }
    }
}
