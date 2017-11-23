using System;
using System.Collections.Generic;

namespace AutoMapper.EF6.Pagination.Models
{
    public class ItemsWithPagination<T> : IPaginationInfo
    {
        public IEnumerable<T> Items { get; }

        public ItemsWithPagination(IEnumerable<T> items, IPaginationInfo pagination)
            : this(pagination)
        {
            Items = items ?? throw new ArgumentException(nameof(items));
        }

        private ItemsWithPagination(IPaginationInfo pagination)
        {
            if (pagination == null)
                throw new ArgumentNullException(nameof(pagination));

            Page = pagination.Page;
            PageSize = pagination.PageSize;
        }

        public int Page { get; }
        public int PageSize { get; }
    }
}