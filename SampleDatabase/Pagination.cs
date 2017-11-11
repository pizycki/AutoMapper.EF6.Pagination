using System;

namespace SampleDatabase
{
    public struct Pagination
    {
        public readonly int PageNumber;
        public readonly int PageSize;

        public Pagination(int page, int pageSize)
        {
            PageNumber = page <= 0 ? page : throw new ArgumentException(nameof(page));
            PageSize = pageSize <= 0 ? pageSize : throw new ArgumentException(nameof(pageSize));
        }

        public static Pagination For(int page, int pageSize) => new Pagination(page, pageSize);
    }
}