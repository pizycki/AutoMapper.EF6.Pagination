using System.Collections.Generic;
using System.Linq;
using AngularExample.EF;
using AutoMapper.EF6.Pagination;
using AutoMapper.EF6.Pagination.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IEnumerable<Customer> GetAllCompanies(AllCustomersQuery query) =>
            _context.Customers
                    .SortAndPaginate(query)
                    .ToList();
    }
}
