using System;
using PagiNET.Paginate;

namespace PagiNET
{
    public class PageAndPager<T> : Page<T>
    {
        public virtual int PagesTotal { get; set; }

        public PageAndPager(Page<T> page, int pagesTotal) : base(page.Items, page)
        {
            if (pagesTotal < 0) throw new ArgumentException("Total count of pages cannot be negative number.");
            if (pagesTotal < page.Number) throw new ArgumentException("Total count of pages cannot be less then actual number.");

            PagesTotal = pagesTotal;
        }
    }
}
