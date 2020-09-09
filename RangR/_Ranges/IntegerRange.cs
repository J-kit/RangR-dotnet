using RangR.Utils;

using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace RangR
{
    public class IntegerRange : RangeBase<int>
    {
        public IntegerRange(int start, int end) : base(start, end)
        {
        }

        public override bool TryMerge(int item, int maxInterval = 1)
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

            var offset = System.Math.Abs(value - item);
            if (offset != 0 && offset <= maxInterval)
            {
                setValue(item);
                return true;
            }
            return false;
        }

        public override bool TryMerge(RangeBase<int> other, int maxInterval = 1)
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

            if (Math.Abs(base.Start - other.End) > maxInterval && Math.Abs(base.End - other.Start) > maxInterval)
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

        public new static IEnumerable<IntegerRange> FromEnumerable(IEnumerable<int> enumerable, int maxInterval = 1)
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

            return ranges.MergeIntersecting(maxInterval);
        }
    }
}