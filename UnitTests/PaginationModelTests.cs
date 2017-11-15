using System;
using AutoMapper.EF6.Pagination.Models;
using NUnit.Framework;
using Shouldly;

namespace UnitTests
{
    public class PaginationModelTests
    {
        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void should_not_accept_invalid_page_argument(int page) =>
            Should.Throw<ArgumentException>(() => Pagination.Set(page, 1));

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void should_not_accept_invalid_page_size_argument(int pageSize) =>
            Should.Throw<ArgumentException>(() => Pagination.Set(1, pageSize));
    }
}
