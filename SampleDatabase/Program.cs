using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using SampleDatabase.DbContext;
using SampleDatabase.DbContext.Entities;

namespace SampleDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new SampleDbContext();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Bar, BarDTO>();
            });

            var page = 5;
            var pageSize = 10;

            var barDTOs = ctx.Bars.AsQueryable()
                .SortAndPaginate(Ascending<Bar, Guid>.By(x => x.FooId), Pagination.For(page: 2, pageSize: 20))
                .ProjectTo<BarDTO>(config)
                .ToList();

            Console.WriteLine($"collection size {barDTOs.Count}");
            Console.WriteLine(barDTOs.Last().Value);

            Console.ReadKey();
        }
    }

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

    public class Sorting<T, K>
    {
        public readonly Expression<Func<T, K>> OrderBy;
        public readonly bool Descending;

        protected Sorting(Expression<Func<T, K>> orderBy, bool descending)
        {
            OrderBy = orderBy ?? throw new ArgumentException(nameof(orderBy));
            Descending = descending;
        }
    }

    public class Ascending<TEntity, TSort> : Sorting<TEntity, TSort>
    {
        public Ascending(Expression<Func<TEntity, TSort>> orderBy) : base(orderBy, false) { }

        public static Ascending<TEntity, TSort> By(Expression<Func<TEntity, TSort>> orderBy) => new Ascending<TEntity, TSort>(orderBy);
    }


    public class Descending<TEntity, TSort> : Sorting<TEntity, TSort>
    {
        public Descending(Expression<Func<TEntity, TSort>> orderBy) : base(orderBy, true) { }

        public static Descending<TEntity, TSort> Create(Expression<Func<TEntity, TSort>> orderBy) => new Descending<TEntity, TSort>(orderBy);
    }

    public struct Pagination
    {
        public readonly int PageNumber;
        public readonly int PageSize;

        public Pagination(int page, int pageSize)
        {
            PageNumber = page <= 0 ? page : throw new ArgumentException(nameof(page));
            PageSize = pageSize <= 0 ? pageSize : throw new ArgumentException(nameof(pageSize));
        }

        public static Pagination For(int page, int pageSize)
        {
            return new Pagination(page, pageSize);
        }
    }
}