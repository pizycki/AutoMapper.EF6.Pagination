using System;
using Shouldly;
using Xunit;

namespace PagiNET.IntegrationTests.Utilities
{
    public static class Comparers
    {
        public static Func<long, long, int> LongsComparer =>
            (a, b) => a > b ? 1
                    : a < b ? -1
                    : 0;

        public static Func<long, long, bool> LongsEquals => (a, b) => a == b;

    }

    public class LongsComparersTests
    {
        private const int FirstGreater = 1;
        private const int FirstSmaller = -1;
        private const int BothEqual = 0;

        [Theory]
        [InlineData(1L, 2L, FirstSmaller)]
        [InlineData(0L, 0L, BothEqual)]
        [InlineData(1L, -1L, FirstGreater)]
        public void can_compare_two_values(long a, long b, int result) =>
            Comparers.LongsComparer(a, b).ShouldBe(result);
    }

    public class LongsEqualsTests
    {
        private const bool Equal = true;
        private const bool Unequal = false;

        [Theory]
        [InlineData(1, 2, Unequal)]
        [InlineData(1, 1, Equal)]
        public void can_compare_two_values(long a, long b, bool result) =>
            Comparers.LongsEquals(a, b).ShouldBe(result);
    }

}
