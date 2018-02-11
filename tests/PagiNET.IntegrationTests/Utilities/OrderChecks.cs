using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;
using Xunit;
using static PagiNET.IntegrationTests.Utilities.ItemIsTheBiggest;

namespace PagiNET.IntegrationTests.Utilities
{
    public static class OrderChecks
    {

        public static bool IsOrderedAscending<TItem>(
            IEnumerable<TItem> items,
            Func<TItem, TItem, int> compare,
            Func<TItem, TItem, bool> equal)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (!items.Any()) throw new ArgumentException("Collection is empty.");

            return items
                .Select(x => items.SkipWhile(y => ReferenceEquals(x, y)))
                .Select(coll => IsLastItemTheBiggest(coll, compare, equal))
                .All(result => result == true);
        }


        public static bool IsOrderedDescending<TItem>(
            IEnumerable<TItem> items,
            Func<TItem, TItem, int> compare,
            Func<TItem, TItem, bool> equal)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (!items.Any()) throw new ArgumentException("Collection is empty.");

            return items
                .Select(x => items.SkipWhile(y => ReferenceEquals(x, y)))
                .Select(coll => IsFirstItemTheBiggest(coll, compare, equal))
                .All(result => result == true);
        }
    }

    public class OrderCheckerTests
    {
        [Fact]
        public void checking_order_of_ordered_asc_collection_works() =>
            OrderChecks.IsOrderedAscending(
                new long[] { 1, 2, 3 },
                Comparers.LongsComparer,
                Comparers.LongsEquals).ShouldBeTrue();

        [Fact]
        public void checking_order_of_unordered_asc_collection_works() =>
            OrderChecks.IsOrderedAscending(
                new long[] { 1, 3, 2 },
                Comparers.LongsComparer,
                Comparers.LongsEquals).ShouldBeFalse();

        [Fact]
        public void checking_order_of_ordered_desc_collection_works() =>
            OrderChecks.IsOrderedDescending(
                new long[] { 3, 2, 1 },
                Comparers.LongsComparer,
                Comparers.LongsEquals).ShouldBeTrue();

        [Fact]
        public void checking_order_of_unordered_desc_collection_works() =>
            OrderChecks.IsOrderedAscending(
                new long[] { 1, 3, 2 },
                Comparers.LongsComparer,
                Comparers.LongsEquals).ShouldBeFalse();

    }

}
