using System;
using System.Linq;
using System.Linq.Expressions;
using PagiNET.Paginate;
using PagiNET.Sort;

namespace PagiNET.Queryable
{
    /// <summary>
    /// Extension methods for queryable sorting and pagination.
    /// </summary>
    public static class SortAndTakePageExtensions
    {
        public static IQueryable<T> SortAndTakePage<T, K>(this IQueryable<T> queryable, Sorting<T, K> sorting, Pagination pagination) =>
            queryable
                .Sort(sorting)
                .TakePage(pagination);

        public static IQueryable<T> SortAndTakePage<T>(this IQueryable<T> queryable, Sorting<T> sorting, Pagination pagination) =>
            queryable
                .Sort(sorting)
                .TakePage(pagination);

        public static IQueryable<T> SortAndTakePage<T>(this IQueryable<T> queryable, IQueryWithPage query) =>
            queryable.SortAndTakePage(query.CreateSortingAndPagination<T>());

        public static IQueryable<T> SortAndTakePage<T, K>(this IQueryable<T> queryable, (Sorting<T, K> sorting, Pagination pagination) snp) =>
            queryable.SortAndTakePage(snp.sorting, snp.pagination);

        public static IQueryable<T> SortAndTakePage<T>(this IQueryable<T> queryable, (Sorting<T> sorting, Pagination pagination) snp) =>
            queryable.SortAndTakePage(snp.sorting, snp.pagination);

        public static IQueryable<T> SortAndTakePage<T, K>(this IQueryable<T> queryable, int page, int pageSize, Expression<Func<T, K>> columnToOrderBy, bool descending = false)
        {
            var pagination = Pagination.Set(page, pageSize);
            var sorting = descending
                ? Descending<T, K>.By(columnToOrderBy).AsSorting()
                : Ascending<T, K>.By(columnToOrderBy).AsSorting();
            return queryable.SortAndTakePage(sorting, pagination);
        }

        public static IQueryable<T> SortAndTakePage<T>(this IQueryable<T> queryable, int page, int pageSize, string columnToOrderBy, bool descending = false)
        {
            var pagination = Pagination.Set(page, pageSize);
            var sorting = new Sorting<T>(columnToOrderBy, descending);
            return queryable.SortAndTakePage(sorting, pagination);
        }
    }

    /// <summary>
    /// Sorting extension methods.
    /// </summary>
    public static class SortExtensions
    {
        internal static IOrderedQueryable<T> Sort<T>(this IQueryable<T> queryable, Sorting<T> sorting) =>
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

        internal static IOrderedQueryable<T> Sort<T, K>(this IQueryable<T> queryable, Sorting<T, K> sorting) =>
            sorting.Descending
                ? queryable.OrderByDescending(sorting.OrderBy)
                : queryable.OrderBy(sorting.OrderBy);
    }

    /// <summary>
    /// Take&Skip extension methods.
    /// </summary>
    public static class TakePageExtensions
    {
        public static IQueryable<T> TakePage<T>(this IQueryable<T> queryable, Pagination pagination) =>
            queryable
                .Skip(pagination.CalculateNumberOfItemsToSkip())
                .Take(pagination.PageSize);

        public static IQueryable<T> TakePage<T>(this IQueryable<T> queryable, IQueryWithPage query) =>
            queryable.TakePage(query.CreatePagination<T>());
    }
}