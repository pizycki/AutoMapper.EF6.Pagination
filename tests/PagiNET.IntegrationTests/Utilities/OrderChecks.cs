using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;
using Xunit;

namespace PagiNET.IntegrationTests.Utilities
{
    public static class OrderChecks
    {
        public static bool IsFirstItemTheBiggest<TItem>(
            IEnumerable<TItem> items,
            Func<TItem, TItem, int> compare,
            Func<TItem, TItem, bool> equal)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (!items.Any()) throw new ArgumentException("Collection is empty.");

            var max = items.Aggregate((a, b) => compare(a, b) != -1 ? a : b);

            // Assert if max is the last element in collection
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

            // Assert if max is the last element in collection
            return equal(items.Last(), max);
        }

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
                new[] { 1L, 2L, 3L },
                Comparers.LongsComparer,
                Comparers.LongsEquals).ShouldBeTrue();


        [Fact]
        public void checking_order_of_unordered_asc_collection_works() =>
            OrderChecks.IsOrderedAscending(
                new[] { 1L, 3L, 2L },
                Comparers.LongsComparer,
                Comparers.LongsEquals).ShouldBeFalse();

        [Fact]
        public void checking_order_of_ordered_desc_collection_works() =>
            OrderChecks.IsOrderedDescending(
                new[] { 3L, 2L, 1L },
                Comparers.LongsComparer,
                Comparers.LongsEquals).ShouldBeTrue();


        [Fact]
        public void checking_order_of_unordered_desc_collection_works() =>
            OrderChecks.IsOrderedAscending(
                new[] { 1L, 3L, 2L },
                Comparers.LongsComparer,
                Comparers.LongsEquals).ShouldBeFalse();

    }
}
