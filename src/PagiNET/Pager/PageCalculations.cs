using System;
using PagiNET.Paginate;

namespace PagiNET.Pager
{
    public static class PageCalculations
    {
        public static int CountPagesTotal(IPageInfo page, int itemsTotal) => (int)Math.Ceiling((double)(itemsTotal / page.Size));

        public static int CalculateItemsNumberToSkip(IPageInfo pagination) => (pagination.Number - 1) * pagination.Size;
    }
}