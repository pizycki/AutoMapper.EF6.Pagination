using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;
using Xunit;
using static PagiNET.IntegrationTests.Utilities.ItemIsTheBiggest;

namespace PagiNET.IntegrationTests.Utilities
{
    public static class ItemIsTheBiggest
    {
        public static bool IsFirstItemTheBiggest<TItem>(
            IEnumerable<TItem> items,
            Func<TItem, TItem, int> compare,
            Func<TItem, TItem, bool> equal)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (!items.Any()) throw new ArgumentException("Collection is empty.");

            var max = items.Aggregate((a, b) => compare(a, b) != -1 ? a : b);

            return equal(items.First(), max);
        }

        public static bool IsLastItemTheBiggest<TItem>(
            IEnumerable<TItem> items,
            Func<TItem, TItem, int> compare,
            Func<TItem, TItem, bool> equal)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (!items.Any()) throw new ArgumentException("Collection is empty.");

            var max = items.Aggregate((a, b) => compare(a, b) != 1 ? b : a);

            return equal(items.Last(), max);
        }
    }

    public class ItemIsTheBiggestTests
    {
        [Fact]
        public void checking_the_first_item_is_the_biggest_works() =>
            IsFirstItemTheBiggest(
                items: new long[] { 5, 4, 3, 3, 2, 1 },
                compare: Comparers.LongsComparer,
                equal: Comparers.LongsEquals).ShouldBeTrue();

        [Fact]
        public void checking_the_last_item_is_the_biggest_works() =>
            IsLastItemTheBiggest(
                items: new long[] { 1, 2, 3, 3, 4, 5 },
                compare: Comparers.LongsComparer,
                equal: Comparers.LongsEquals).ShouldBeTrue();


        [Fact]
        public void checking_the_first_item_is_the_biggest_but_is_not_works() =>
            IsFirstItemTheBiggest(
                items: new long[] { 2, 6, 3, 2, 6, 4 },
                compare: Comparers.LongsComparer,
                equal: Comparers.LongsEquals).ShouldBeFalse();

        [Fact]
        public void checking_the_last_item_is_the_biggest_but_is_not_works() =>
            IsLastItemTheBiggest(
                items: new long[] { 2, 6, 3, 2, 6, 4 },
                compare: Comparers.LongsComparer,
                equal: Comparers.LongsEquals).ShouldBeFalse();
    }
}