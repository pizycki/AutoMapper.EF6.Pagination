using System;
using System.Collections;
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
        public ItemsWithPagination<Customer> GetAllCompanies(AllCustomersQuery query) =>
            _context.Customers
                    .SortAndPaginate(query)
                    .ToList()
                    .ToItemsWithPagination(query);
    }

    public static class ItemsWithPaginationExtensions
    {
        public static ItemsWithPagination<T> ToItemsWithPagination<T>(this IEnumerable<T> collection, IPaginationInfo pagination) =>
            new ItemsWithPagination<T>(collection, pagination);
    }

    public class ItemsWithPagination<T> : IPaginationInfo
    {
        public IEnumerable<T> Items { get; }

        public ItemsWithPagination(IEnumerable<T> items, IPaginationInfo pagination)
            : this(pagination)
        {
            Items = items ?? throw new ArgumentException(nameof(items));
        }

        private ItemsWithPagination(IPaginationInfo pagination)
        {
            if (pagination == null)
                throw new ArgumentNullException(nameof(pagination));

            Page = pagination.Page;
            PageSize = pagination.PageSize;
        }

        public int Page { get; }
        public int PageSize { get; }
    }
}
