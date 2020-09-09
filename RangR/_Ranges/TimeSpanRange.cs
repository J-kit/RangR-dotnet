using RangR.Maths.Absolution;

using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace RangR
{
    public class TimeSpanRange : RangeBase<TimeSpan>
    {
        public TimeSpanRange(TimeSpan start, TimeSpan end, Comparer<TimeSpan> comparer = null)
            : base(start, end, comparer)
        {
        }

        public TimeSpanRange(long startTicks, long endTicks, Comparer<TimeSpan> comparer = null)
            : this(new TimeSpan(startTicks), new TimeSpan(endTicks), comparer)
        {
        }

        public TimeSpanRange(RangeBase<long> range, Comparer<TimeSpan> comparer = null) : this(range.Start, range.End, comparer)
        {
        }

        public static IEnumerable<TimeSpanRange> FromEnumerable(IEnumerable<TimeSpan> enumerable, int maxInterval = 1)
        {
            return RangeBase<long>.FromEnumerable(enumerable.Select(x => x.Ticks), maxInterval).Select(x => new TimeSpanRange(x));
        }

        public IEnumerable<TimeSpan> Enumerate(TimeSpan interval)
        {
            var realInterval = Absolutor<TimeSpan>.Default.Abs(interval);

            var item = this.Start;
            yield return item;

            while (item < this.End)
            {
                item += realInterval;
                yield return item;
            }
        }

        public TimeSpanRange RoundMinutes(int minuteInterval)
        {
            return new TimeSpanRange
            (
                RoundToNearestMinutes(this.Start, 15),
                RoundToNearestMinutes(this.End, 15)
            );
        }

        private static TimeSpan RoundToNearestMinutes(TimeSpan input, int minutes)
        {
            var totalMinutes = (int)(input + new TimeSpan(0, minutes / 2, 0)).TotalMinutes;
            return new TimeSpan(0, totalMinutes - totalMinutes % minutes, 0);
        }
    }
}