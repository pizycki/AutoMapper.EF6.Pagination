using System;
using System.Linq;
using System.Linq.Expressions;
using PagiNET.Paginate;
using PagiNET.Sort;

namespace PagiNET.Queryable
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> SortAndPaginate<T, K>(this IQueryable<T> queryable, Sorting<T, K> sorting, Pagination pagination) =>
            queryable
                .Sort(sorting)
                .Paginate(pagination);

        public static IQueryable<T> SortAndPaginate<T>(this IQueryable<T> queryable, Sorting<T> sorting, Pagination pagination) =>
            queryable
                .Sort(sorting)
                .Paginate(pagination);

        public static IQueryable<T> SortAndPaginate<T>(this IQueryable<T> queryable, IQueryWithPage query) =>
            queryable.SortAndPaginate(query.CreatePagination<T>());

        public static IQueryable<T> SortAndPaginate<T, K>(this IQueryable<T> queryable, (Sorting<T, K> sorting, Pagination pagination) snp) =>
            queryable.SortAndPaginate(snp.sorting, snp.pagination);

        public static IQueryable<T> SortAndPaginate<T>(this IQueryable<T> queryable, (Sorting<T> sorting, Pagination pagination) snp) =>
            queryable.SortAndPaginate(snp.sorting, snp.pagination);

        public static IQueryable<T> SortAndPaginate<T, K>(this IQueryable<T> queryable, int page, int pageSize, Expression<Func<T, K>> columnToOrderBy, bool descending = false)
        {
            var pagination = Pagination.Set(page, pageSize);
            var sorting = Ascending<T, K>.By(columnToOrderBy);
            return queryable.SortAndPaginate(sorting, pagination);
        }

        public static IQueryable<T> SortAndPaginate<T>(this IQueryable<T> queryable, int page, int pageSize, string columnToOrderBy, bool descending = false)
        {
            var pagination = Pagination.Set(page, pageSize);
            var sorting = new Sorting<T>(columnToOrderBy, descending);
            return queryable.SortAndPaginate(sorting, pagination);
        }

        private static IOrderedQueryable<T> Sort<T>(this IQueryable<T> queryable, Sorting<T> sorting) =>
            (IOrderedQueryable<T>)queryable.Provider.CreateQuery<T>(
                Expression.Call(
                    type: typeof(System.Linq.Queryable),
                    methodName: sorting.Descending ? nameof(System.Linq.Queryable.OrderByDescending) : nameof(System.Linq.Queryable.OrderBy),
                    typeArguments: new[] { typeof(T), sorting.ColumnType },
                    arguments: new[]
                    {
                        queryable.Expression,
                        Expression.Quote(sorting.OrderBy)
                    }));


        private static IOrderedQueryable<T> Sort<T, K>(this IQueryable<T> queryable, Sorting<T, K> sorting) =>
            sorting.Descending
                ? queryable.OrderByDescending(sorting.OrderBy)
                : queryable.OrderBy(sorting.OrderBy);

        private static IQueryable<T> Paginate<T>(this IOrderedQueryable<T> queryable, Pagination pagination) =>
            queryable
                .Skip(pagination.CalculateNumberOfItemsToSkip())
                .Take(pagination.PageSize);
    }
}