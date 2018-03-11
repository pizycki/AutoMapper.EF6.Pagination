using System;

namespace PagiNET.Paginate
{
    public struct Pagination : IPageInfo
    {
        public readonly int PageNumber;
        public readonly int PageSize;

        int IPageInfo.Number => PageNumber;
        int IPageInfo.Size => PageSize;

        public Pagination(int page, int pageSize)
        {
            PageNumber = page > 0 ? page : throw new ArgumentException(nameof(page));
            PageSize = pageSize > 0 ? pageSize : throw new ArgumentException(nameof(pageSize));
        }

        public static Pagination Set(int page, int pageSize) => new Pagination(page, pageSize);
    }

    public static class PaginationExtensions
    {
        internal static int CalculateItemsNumberToSkip(this IPageInfo pagination) =>
            (pagination.Number - 1) * pagination.Size;
    }
}