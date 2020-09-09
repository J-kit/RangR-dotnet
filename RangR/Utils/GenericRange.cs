using System.Collections.Generic;
using System.Linq;

namespace RangR.Utils
{
    public static class GenericRange
    {
        public static List<TRange> MergeIntersecting<TRange, TValue>(this IEnumerable<TRange> ranges, TValue maxInterval) where TRange : RangeBase<TValue>
        {
            var enumerated = ranges.ToList();

            var targetRanges = new List<TRange>(enumerated.Count);
            foreach (var range in enumerated.Where(range => !targetRanges.Any(x => x.TryMerge(range, maxInterval))))
            {
                targetRanges.Add(range);
            }

            return targetRanges;
        }
    }
}