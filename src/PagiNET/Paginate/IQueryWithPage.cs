using System;
using PagiNET.Pager;
using PagiNET.Sort;

namespace PagiNET.Paginate
{
    public interface IQueryWithPage : IPageInfo, ISortInfo { }

    public static class IQueryWithPaginationExtensions
    {
        public static (Sorting<T> sorting, Pagination pagination) CreatePagination<T>(this IQueryWithPage query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var pagination = Pagination.Set(query.Number, query.Size);
            var sorting = Sorting<T>.By(query.OrderBy, query.Descending);

            return (sorting, pagination);
        }

        public static PagedResult<TItem> GetPaginatedResult<TItem>(
            this IQueryWithPage query,
            Func<IQueryWithPage, Page<TItem>> getItems,
            Func<IQueryWithPage, PagerModel> getPager)
        {
            if (getItems == null) throw new ArgumentNullException(nameof(getItems));
            if (getPager == null) throw new ArgumentNullException(nameof(getPager));

            return new PagedResult<TItem>(getItems(query), getPager(query));
        }
    }
}