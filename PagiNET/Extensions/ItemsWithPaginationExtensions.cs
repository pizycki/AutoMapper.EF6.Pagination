using System.Collections.Generic;
using PagiNET.Models;

namespace PagiNET.Extensions
{
    public static class ItemsWithPaginationExtensions
    {
        public static ItemsWithPagination<T> ToItemsWithPagination<T>(this IEnumerable<T> collection, IPaginationInfo pagination) =>
            new ItemsWithPagination<T>(collection, pagination);
    }
}