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
        public static IQueryable<T> TakeSortedPage<T, K>(this IQueryable<T> queryable, Sorting<T, K> sorting, Pagination pagination) =>
            queryable
                .Sort(sorting)
                .TakePage(pagination);

        public static IQueryable<T> TakeSortedPage<T>(this IQueryable<T> queryable, Sorting<T> sorting, Pagination pagination) =>
            queryable
                .Sort(sorting)
                .TakePage(pagination);

        public static IQueryable<T> TakeSortedPage<T>(this IQueryable<T> queryable, IQueryWithPage query) => queryable.TakeSortedPage(query.CreateSortingAndPagination<T>());

        public static IQueryable<T> TakeSortedPage<T, K>(this IQueryable<T> queryable, (Sorting<T, K> sorting, Pagination pagination) snp) => queryable.TakeSortedPage(snp.sorting, snp.pagination);

        public static IQueryable<T> TakeSortedPage<T>(this IQueryable<T> queryable, (Sorting<T> sorting, Pagination pagination) snp) => queryable.TakeSortedPage(snp.sorting, snp.pagination);

        public static IQueryable<T> TakeSortedPage<T, K>(this IQueryable<T> queryable, int page, int pageSize, Expression<Func<T, K>> columnToOrderBy, bool descending = false) =>
            queryable.TakeSortedPage(sorting: descending ? Descending<T, K>.By(columnToOrderBy).AsSorting()
                                                         : Ascending<T, K>.By(columnToOrderBy).AsSorting(),
                                     pagination: Pagination.Set(page, pageSize));

        public static IQueryable<T> TakeSortedPage<T>(this IQueryable<T> queryable, int page, int pageSize, string columnToOrderBy, bool descending = false)
        {
            var pagination = Pagination.Set(page, pageSize);
            var sorting = new Sorting<T>(columnToOrderBy, descending);
            return queryable.TakeSortedPage(sorting, pagination);
        }

        public static Page<T> SingleSortedPage<T, K>(this IQueryable<T> queryable, Sorting<T, K> sorting, Pagination pagination) =>
            queryable
                .TakeSortedPage(sorting, pagination)
                .ToList()
                .AsPage(pagination);

        public static Page<T> SingleSortedPage<T>(this IQueryable<T> queryable, Sorting<T> sorting, Pagination pagination) =>
            queryable
                .TakeSortedPage(sorting, pagination)
                .ToList()
                .AsPage(pagination);

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
                .Skip(pagination.CalculateItemsNumberToSkip())
                .Take(pagination.PageSize);

        public static IQueryable<T> TakePage<T>(this IQueryable<T> queryable, IPageInfo query) =>
            queryable.TakePage(query.CreatePagination<T>());

        public static Page<T> SinglePage<T>(this IQueryable<T> queryable, IPageInfo pageInfo) =>
            queryable
                .TakePage(pageInfo.CreatePagination<T>())
                .ToList()
                .AsPage(pageInfo);
    }
}