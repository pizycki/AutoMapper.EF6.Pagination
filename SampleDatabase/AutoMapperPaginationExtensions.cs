using System;
using System.Linq;
using System.Linq.Expressions;

namespace SampleDatabase
{
    public static class AutoMapperPaginationExtensions
    {
        public static IQueryable<T> SortAndPaginate<T, K>(this IQueryable<T> queryable, int page, int pageSize, Expression<Func<T, K>> orderBy, bool descending = false)
        {
            var pagination = Pagination.For(page, pageSize);
            var sorting = new Ascending<T, K>(orderBy);
            return queryable.SortAndPaginate(sorting, pagination);
        }

        public static IQueryable<T> SortAndPaginate<T, K>(this IQueryable<T> queryable, Sorting<T, K> sorting, Pagination pagination) =>
            queryable
                .Sort(sorting)
                .Paginate(pagination);

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