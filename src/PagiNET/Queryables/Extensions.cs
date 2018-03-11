using System;
using System.Linq;
using System.Linq.Expressions;
using PagiNET.Pager;
using PagiNET.Paginate;
using PagiNET.Sort;

namespace PagiNET.Queryables
{
    /// <summary>
    /// Pagination extensions for <see cref="IQueryable{T}"/>.
    /// The extensions accept <see cref="Sorting{T}"/> which is desinged to sort collections by name of the entity property, provided as <see cref="string"/>.
    /// </summary>
    public static partial class SortedPageExtensions
    {
        internal static IOrderedQueryable<T> Sort<T>(this IQueryable<T> queryable, Sorting<T> sorting) =>
            (IOrderedQueryable<T>)queryable.Provider.CreateQuery<T>(
                Expression.Call(
                    type: typeof(Queryable),
                    methodName: sorting.Descending ? nameof(Queryable.OrderByDescending) : nameof(Queryable.OrderBy),
                    typeArguments: new[] { typeof(T), sorting.ColumnType },
                    arguments: new[]
                    {
                        queryable.Expression,
                        Expression.Quote(sorting.OrderBy)
                    }));


        #region ////////// Take Sorted Page //////////

        public static IQueryable<T> TakeSortedPage<T>(this IQueryable<T> queryable, IQueryWithPage query) => queryable.TakeSortedPage(query.CreateSortingAndPagination<T>());

        public static IQueryable<T> TakeSortedPage<T>(this IQueryable<T> queryable, Sorting<T> sorting, IPageInfo pagination) => queryable.Sort(sorting).TakePage(pagination);

        public static IQueryable<T> TakeSortedPage<T>(this IQueryable<T> queryable, (Sorting<T> sorting, Pagination pagination) snp) => queryable.TakeSortedPage(snp.sorting, snp.pagination);

        public static IQueryable<T> TakeSortedPage<T>(this IQueryable<T> queryable, int page, int pageSize, string orderBy, bool descending = false) =>
            queryable.TakeSortedPage(sorting: descending ? Descending<T>.By(orderBy).AsSorting()
                                                         : Ascending<T>.By(orderBy).AsSorting(),
                                     pagination: Pagination.Set(page, pageSize));

        #endregion

        #region ////////// Single Sorted Page //////////

        public static Page<T> SingleSortedPage<T>(this IQueryable<T> queryable, Sorting<T> sorting, Pagination pagination) => queryable.TakeSortedPage(sorting, pagination).ToList().AsPage(pagination);

        #endregion
    }

    /// <summary>
    /// Pagination extensions for <see cref="IQueryable{T}"/>.
    /// The extensions accept <see cref="Sorting{T,K}"/> which is desinged to sort collections by name of the entity property, provided as <see cref="Expression"/>.
    /// </summary>
    public static partial class SortedPageExtensions
    {
        internal static IOrderedQueryable<T> Sort<T, K>(this IQueryable<T> queryable, Sorting<T, K> sorting) => sorting.Descending ? queryable.OrderByDescending(sorting.OrderBy) : queryable.OrderBy(sorting.OrderBy);

        #region ////////// Take Sorted Page //////////

        public static IQueryable<T> TakeSortedPage<T, K>(this IQueryable<T> queryable, Sorting<T, K> sorting, IPageInfo pagination) => queryable.Sort(sorting).TakePage(pagination);

        public static IQueryable<T> TakeSortedPage<T, K>(this IQueryable<T> queryable, (Sorting<T, K> sorting, Pagination pagination) snp) => queryable.TakeSortedPage(snp.sorting, snp.pagination);

        public static IQueryable<T> TakeSortedPage<T, K>(this IQueryable<T> queryable, int page, int pageSize, Expression<Func<T, K>> orderBy, bool descending = false) =>
            queryable.TakeSortedPage(sorting: descending ? Descending<T, K>.By(orderBy).AsSorting()
                                                         : Ascending<T, K>.By(orderBy).AsSorting(),
                                     pagination: Pagination.Set(page, pageSize));

        #endregion

        #region ////////// Single Sorted Page //////////

        public static Page<T> SingleSortedPage<T, K>(this IQueryable<T> queryable, Sorting<T, K> sorting, Pagination pagination) => queryable.TakeSortedPage(sorting, pagination).ToList().AsPage(pagination);

        #endregion
    }

    /// <summary>
    /// Pagination extensions for <see cref="IQueryable{T}"/>.
    /// 
    /// </summary>
    public static class TakePageExtensions
    {
        public static IQueryable<T> TakePage<T>(this IQueryable<T> queryable, Pagination pagination) => queryable.Skip(PageCalculations.CalculateItemsNumberToSkip(pagination)).Take(pagination.PageSize);

        public static IQueryable<T> TakePage<T>(this IQueryable<T> queryable, IPageInfo query) => queryable.TakePage(query.CreatePagination<T>());

        public static Page<T> SinglePage<T>(this IQueryable<T> queryable, IPageInfo pageInfo) => queryable.TakePage(pageInfo.CreatePagination<T>()).ToList().AsPage(pageInfo);
    }
}