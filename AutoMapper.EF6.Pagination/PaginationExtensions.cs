using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.EF6.Pagination.Models;

namespace AutoMapper.EF6.Pagination
{
    using Pagination = Models.Pagination;

    public static class AutoMapperPaginationExtensions
    {
        public static IQueryable<T> SortAndPaginate<T, K>(this IQueryable<T> queryable, Sorting<T, K> sorting, Pagination pagination) =>
            queryable
                .Sort(sorting)
                .Paginate(pagination);

        public static IQueryable<T> SortAndPaginate<T, K>(this IQueryable<T> queryable, int page, int pageSize, Expression<Func<T, K>> columnToOrderBy, bool descending = false)
        {
            var pagination = Pagination.Set(page, pageSize);
            var sorting = Ascending<T, K>.By(columnToOrderBy);
            return queryable.SortAndPaginate(sorting, pagination);
        }

        private static IOrderedQueryable<T> Sort<T, K>(this IQueryable<T> queryable, Sorting<T, K> sorting) =>
            sorting.Descending
                ? queryable.OrderByDescending(sorting.OrderBy)
                : queryable.OrderBy(sorting.OrderBy);

        private static IQueryable<T> Paginate<T>(this IOrderedQueryable<T> queryable, Pagination pagination) =>
            queryable
                .Skip(pagination.CalculateNumberOfItemsToSkip())
                .Take(pagination.PageSize);

        private static int CalculateNumberOfItemsToSkip(this Pagination pagination) =>
            (pagination.PageNumber - 1) * pagination.PageSize;
    }
}