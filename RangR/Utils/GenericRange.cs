using System;
using System.Collections.Generic;
using System.Linq;

namespace RangR.Utils
{
    public static class GenericRange
    {
        public static List<TRange> MergeIntersecting<TRange, TValue>(this IEnumerable<TRange> ranges, TValue maxInterval) where TRange : RangeBase<TValue>
        {
            var enumerated = ranges as List<TRange> ?? ranges.ToList();

            var targetRanges = new List<TRange>(enumerated.Count);
            foreach (var range in enumerated.Where(range => !targetRanges.Any(x => x.TryMerge(range, maxInterval))))
            {
                targetRanges.Add(range);
            }

            return targetRanges;
        }

        public static List<TRange> MergeIntersecting<TRange>(this IEnumerable<TRange> ranges, TimeSpan maxInterval) where TRange : RangeBase<DateTime>
        {
            return ranges
                .Select(x => new TimeSpanRange(x.Start.Ticks, x.End.Ticks))
                .MergeIntersecting(maxInterval)
                .Select(x => new DateTimeRange(new DateTime(x.Start.Ticks), new DateTime(x.End.Ticks)))
                .OfType<TRange>()
                .ToList();
        }
    }
}