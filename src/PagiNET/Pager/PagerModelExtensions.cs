using System.Linq;
using PagiNET.Paginate;

namespace PagiNET.Pager
{
    public static class PagerModelExtensions
    {
        public static int CountPages<T>(this IQueryable<T> query, IPageInfo page)
        {
            var total = query.Count();
            return PageCalculations.CountPagesTotal(page, total);
        }
    }
}