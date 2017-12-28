using System;
using System.Linq;
using PagiNET.Paginate;

namespace PagiNET.Pager
{
    public static class PagerModelExtensions
    {
        public static int CountPages<T>(this IQueryable<T> query, IPageInfo info)
        {
            var total = query.Count();

            return (int)Math.Ceiling((double)(total / info.Size));
        }
    }
}