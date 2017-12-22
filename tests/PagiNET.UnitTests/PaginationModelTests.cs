using System;
using PagiNET.Paginate;
using Shouldly;
using Xunit;

namespace PagiNET.UnitTests
{
    public class PaginationModelTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void should_not_accept_invalid_page_argument(int page) =>
            Should.Throw<ArgumentException>(() => Pagination.Set(page, 1));

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void should_not_accept_invalid_page_size_argument(int pageSize) =>
            Should.Throw<ArgumentException>(() => Pagination.Set(1, pageSize));
    }
}
