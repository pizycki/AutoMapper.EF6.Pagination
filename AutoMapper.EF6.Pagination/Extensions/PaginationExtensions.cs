using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.EF6.Pagination.Models;

namespace AutoMapper.EF6.Pagination.Extensions
{
    using Pagination = Models.Pagination;

    public static class AutoMapperPaginationExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <example>
        ///     people.SortAndPaginate(Ascending.By(p => p.Age), Pagination.Set(1, 20));
        /// </example> 
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="sorting"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public static IQueryable<T> SortAndPaginate<T, K>(this IQueryable<T> queryable, Sorting<T, K> sorting, Pagination pagination) =>
            queryable
                .Sort(sorting)
                .Paginate(pagination);


        public static IQueryable<T> SortAndPaginate<T>(this IQueryable<T> queryable, Sorting<T> sorting, Pagination pagination) =>
            queryable
                .Sort(sorting)
                .Paginate(pagination);

        /// <summary>
        /// 
        /// </summary>
        /// <example>
        ///     people.SortAndPaginate(allPeopleInSystemQuery);
        /// </example> 
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IQueryable<T> SortAndPaginate<T>(this IQueryable<T> queryable, IQueryWithPagination query) =>
            queryable.SortAndPaginate(query.CreatePagination<T>());

        /// <summary>
        /// 
        /// </summary>
        /// <example>
        ///     var parameters = (Sort: Ascending.By(p => p.Age), Paginate: Pagination.Set(1, 20));
        ///     people.SortAndPaginate(parameters);
        /// </example> 
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="sortingAndPagination"></param>
        /// <returns></returns>
        public static IQueryable<T> SortAndPaginate<T, K>(this IQueryable<T> queryable, (Sorting<T, K> sorting, Pagination pagination) sortingAndPagination) =>
            queryable.SortAndPaginate(sortingAndPagination.sorting, sortingAndPagination.pagination);

        public static IQueryable<T> SortAndPaginate<T>(this IQueryable<T> queryable, (Sorting<T> sorting, Pagination pagination) sortingAndPagination) =>
            queryable.SortAndPaginate(sortingAndPagination.sorting, sortingAndPagination.pagination);

        /// <summary>
        /// 
        /// </summary>
        /// <example>
        ///     people.SortAndPaginate(page: 1, pageSize: 20, columnToOrderBy: p => p.Age, descending: true);
        /// </example> 
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="columnToOrderBy"></param>
        /// <param name="descending"></param>
        /// <returns></returns>
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
                    type: typeof(Queryable),
                    methodName: sorting.Descending ? nameof(Queryable.OrderByDescending) : nameof(Queryable.OrderBy),
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

        internal static int CalculateNumberOfItemsToSkip(this Pagination pagination) =>
            (pagination.PageNumber - 1) * pagination.PageSize;
    }
}