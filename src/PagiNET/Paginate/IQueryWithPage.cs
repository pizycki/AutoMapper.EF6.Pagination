using System;
using PagiNET.Sort;

namespace PagiNET.Paginate
{
    public interface IQueryWithPage : IPageInfo, ISortInfo
    {
        bool IncludeTotalPages { get; set; }
    }

    public static class IQueryWithPaginationExtensions
    {
        public static (Sorting<T> sorting, Pagination pagination) CreateSortingAndPagination<T>(this IQueryWithPage query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var pagination = Pagination.Set(query.Number, query.Size);
            var sorting = Sorting<T>.By(query.OrderBy, query.Descending);

            return (sorting, pagination);
        }

        public static Pagination CreatePagination<T>(this IPageInfo query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var pagination = Pagination.Set(query.Number, query.Size);

            return pagination;
        }

        public static PageAndPager<TItem> GetPageAndTotalPages<TItem>(
            this IPageInfo query,
            Func<IPageInfo, Page<TItem>> page,
            Func<IPageInfo, int> pagesTotal)
        {
            if (page == null) throw new ArgumentNullException(nameof(page));
            if (pagesTotal == null) throw new ArgumentNullException(nameof(pagesTotal));

            return new PageAndPager<TItem>(page(query), pagesTotal(query));
        }

        public static PageAndPager<TItem> GetPageAndTotalPages<TItem>(
            this IPageAndSortInfo query,
            Func<IPageInfo, Page<TItem>> page,
            Func<IPageInfo, int> pagesTotal)
        {
            if (page == null) throw new ArgumentNullException(nameof(page));
            if (pagesTotal == null) throw new ArgumentNullException(nameof(pagesTotal));

            return new PageAndPager<TItem>(page(query), pagesTotal(query));
        }
    }
}