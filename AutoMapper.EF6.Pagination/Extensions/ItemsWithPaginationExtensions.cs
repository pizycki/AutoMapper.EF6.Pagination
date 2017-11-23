using System.Collections.Generic;
using AutoMapper.EF6.Pagination.Models;

namespace AutoMapper.EF6.Pagination.Extensions
{
    public static class ItemsWithPaginationExtensions
    {
        public static ItemsWithPagination<T> ToItemsWithPagination<T>(this IEnumerable<T> collection, IPaginationInfo pagination) =>
            new ItemsWithPagination<T>(collection, pagination);
    }
}