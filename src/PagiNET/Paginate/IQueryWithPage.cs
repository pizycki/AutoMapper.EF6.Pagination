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

        public static Pagination CreatePagination<T>(this IQueryWithPage query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var pagination = Pagination.Set(query.Number, query.Size);

            return pagination;
        }

        public static PageAndPager<TItem> GetPageAndTotalPages<TItem>(
            this IQueryWithPage query,
            Func<IQueryWithPage, Page<TItem>> getPage,
            Func<IQueryWithPage, int> getTotalPages)
        {
            if (getPage == null) throw new ArgumentNullException(nameof(getPage));
            if (getTotalPages == null) throw new ArgumentNullException(nameof(getTotalPages));

            return new PageAndPager<TItem>(getPage(query), getTotalPages(query));
        }
    }
}