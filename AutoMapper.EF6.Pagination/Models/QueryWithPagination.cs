using System;

namespace AutoMapper.EF6.Pagination.Models
{
    public interface IQueryWithPagination
    {
        string OrderBy { get; set; }
        bool Descending { get; set; }
        int Page { get; set; }
        int PageSize { get; set; }
    }

    public static class IQueryWithPaginationExtensions
    {
        public static (Sorting<T> sorting, Pagination pagination) CreatePagination<T>(this IQueryWithPagination query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));
            var pagination = Pagination.Set(query.Page, query.PageSize);
            var sorting = Sorting<T>.Create(query.OrderBy, query.Descending);
            return (sorting, pagination);
        }
    }
}
