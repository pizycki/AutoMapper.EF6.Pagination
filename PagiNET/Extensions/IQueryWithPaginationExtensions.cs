using System;
using PagiNET.Models;

namespace PagiNET.Extensions
{
    public static class IQueryWithPaginationExtensions
    {
        public static (Sorting<T> sorting, Models.Pagination pagination) CreatePagination<T>(this IQueryWithPagination query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));
            var pagination = Models.Pagination.Set(query.Page, query.PageSize);
            var sorting = Sorting<T>.By(query.OrderBy, query.Descending);
            return (sorting, pagination);
        }
    }
}