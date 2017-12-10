using System;
using System.Linq;
using PagiNET.Paginate;

namespace PagiNET.Pager
{
    public static class PagerModelExtensions
    {
        public static PagerModel GetPagerModel<T>(this IQueryable<T> query, IPaginationInfo info)
        {
            var total = query.Count();

            return new PagerModel
            {
                // ReSharper disable once PossibleLossOfFraction
                Pages = (int)Math.Ceiling((double)(total / info.PageSize))
            };
        }
    }
}