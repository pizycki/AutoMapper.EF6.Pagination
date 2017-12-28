using System;
using PagiNET.Pager;
using PagiNET.Paginate;

namespace PagiNET
{
    public class PageAndPager<T>
    {
        public PageAndPager(Page<T> items, PagerModel pager)
        {
            Page = items ?? throw new ArgumentNullException(nameof(items));
            Pager = pager ?? throw new ArgumentNullException(nameof(pager));
        }

        public virtual Page<T> Page { get; }
        public virtual PagerModel Pager { get; }
    }
}
