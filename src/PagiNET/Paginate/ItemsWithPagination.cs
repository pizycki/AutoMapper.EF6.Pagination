using System;
using System.Collections.Generic;

namespace PagiNET.Paginate
{
    public class ItemsWithPagination<T> : IPaginationInfo
    {
        public IEnumerable<T> Items { get; }

        public ItemsWithPagination(IEnumerable<T> items, IPaginationInfo pagination)
            : this(pagination)
        {
            Items = items ?? throw new ArgumentException(nameof(items));
        }

        private ItemsWithPagination(IPaginationInfo pagination)
        {
            if (pagination == null)
                throw new ArgumentNullException(nameof(pagination));

            Page = pagination.Page;
            PageSize = pagination.PageSize;
        }

        public int Page { get; }
        public int PageSize { get; }
    }
    
    public static class ItemsWithPaginationExtensions
    {
        public static ItemsWithPagination<T> ToItemsWithPagination<T>(this IEnumerable<T> collection, IPaginationInfo pagination) =>
            new ItemsWithPagination<T>(collection, pagination);
    }
}