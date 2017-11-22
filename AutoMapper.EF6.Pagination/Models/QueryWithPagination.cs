using System;

namespace AutoMapper.EF6.Pagination.Models
{
    public interface IPaginationInfo
    {
        int Page { get; }
        int PageSize { get; }
    }

    public interface ISortingInfo
    {
        string OrderBy { get; set; }
        bool Descending { get; set; }
    }

    public interface IQueryWithPagination : IPaginationInfo, ISortingInfo { }

    public static class IQueryWithPaginationExtensions
    {
        public static (Sorting<T> sorting, Pagination pagination) CreatePagination<T>(this IQueryWithPagination query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));
            var pagination = Pagination.Set(query.Page, query.PageSize);
            var sorting = Sorting<T>.By(query.OrderBy, query.Descending);
            return (sorting, pagination);
        }
    }
}
