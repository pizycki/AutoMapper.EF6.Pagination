using System;

namespace AutoMapper.EF6.Pagination.Models
{
    public interface IQueryWithPagination
    {
        string OrderBy { get; set; }
        bool Descending { get; set; }
        int Page { get; set; }
        int PageSize { get; set; }
    }

    public static class IQueryWithPaginationExtensions
    {
        public static (Sorting<T, K> sorting, Pagination pagination) CreatePagination<T, K>(this IQueryWithPagination query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var pagination = Pagination.Set(query.Page, query.PageSize);

            // TODO Parse lambda dynamicly
            //https://stackoverflow.com/questions/821365/how-to-convert-a-string-to-its-equivalent-linq-expression-tree
            //https://weblogs.asp.net/scottgu/dynamic-linq-part-1-using-the-linq-dynamic-query-library

            throw new NotImplementedException();
        }
    }
}
