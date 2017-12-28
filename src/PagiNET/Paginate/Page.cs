using System;
using System.Collections.Generic;

namespace PagiNET.Paginate
{
    public class Page<T> : IPageInfo
    {
        public int Number { get; }
        public int Size { get; }
        public IEnumerable<T> Items { get; }

        public Page(IEnumerable<T> items, IPageInfo page)
            : this(page)
        {
            Items = items ?? throw new ArgumentException(nameof(items));
        }

        private Page(IPageInfo page)
        {
            if (page == null)
                throw new ArgumentNullException(nameof(page));

            Number = page.Number;
            Size = page.Size;
        }
    }

    public static class ItemsWithPaginationExtensions
    {
        public static Page<T> ToItemsWithPagination<T>(this IEnumerable<T> collection, IPageInfo page) =>
            new Page<T>(collection, page);
    }
}