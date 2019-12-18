using System;
using System.Collections.Generic;
using System.Linq;

namespace Ranging
{
    public class IntegerRange : RangeBase<int>
    {
        public IntegerRange(int start, int end) : base(start, end)
        {
        }

        public bool TryMerge(int item, int maxInterval = 1)
        {
            if (base.Contains(item))
            {
                return true;
            }

            int value;
            Action<int> setValue;

            if (item < base.Start)
            {
                value = base.Start;
                setValue = x => base.Start = x;
            }
            else if (item > base.End)
            {
                value = base.End;
                setValue = x => base.End = x;
            }
            else
            {
                return true;
            }

            var offset = Math.Abs(value - item);
            if (offset != 0 && offset <= maxInterval)
            {
                setValue(item);
                return true;
            }
            return false;
        }

        public bool TryMerge(IntegerRange other, int maxInterval = 1)
        {
            if (!this.IsValid || !other.IsValid)
            {
                throw new ArgumentException("Invalid range");
            }

            if (base.Intersects(other))
            {
                base.Start = Math.Min(base.Start, other.Start);
                base.End = Math.Max(base.End, other.End);
                return true;
            }

            if (Math.Abs(base.Start - other.End) > maxInterval)
            {
                return false;
            }

            if (base.Start >= other.End)
            {
                base.Start = other.Start;
                return true;
            }
            else if (base.End <= other.Start)
            {
                base.End = other.End;
                return true;
            }

            return false;
        }

        public static IEnumerable<IntegerRange> FromEnumerable(IEnumerable<int> enumerable, int maxInterval = 1)
        {
            var ranges = new List<IntegerRange>();

            IntegerRange currentRange = null;

            foreach (var item in enumerable)
            {
                if (currentRange == null)
                {
                    currentRange = new IntegerRange(item, item);
                    ranges.Add(currentRange);
                    continue;
                }

                if (currentRange.TryMerge(item, maxInterval))
                {
                    continue;
                }

                var range = ranges.FirstOrDefault(x => x != currentRange && x.TryMerge(item, maxInterval));
                if (range != null)
                {
                    currentRange = range;
                    continue;
                }

                currentRange = new IntegerRange(item, item);
                ranges.Add(currentRange);
            }

            var targetRanges = new List<IntegerRange>(ranges.Count);
            foreach (var range in ranges.Where(range => !targetRanges.Any(x => x.TryMerge(range))))
            {
                targetRanges.Add(range);
            }

            return targetRanges;
        }
    }
}