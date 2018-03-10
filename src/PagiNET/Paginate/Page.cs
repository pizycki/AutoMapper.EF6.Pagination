using System;
using System.Collections.Generic;
using System.Linq;

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
            Items = Enumerable.Empty<T>();
        }
    }

    public static class PageExtensions
    {
        public static Page<T> AsPage<T>(this IEnumerable<T> collection, IPageInfo pageInfo) => new Page<T>(collection, pageInfo);
    }
}