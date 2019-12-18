using System;
using System.Collections.Generic;
using System.Text;
using Ranging.Abstractions;
using Ranging.Extensions;

namespace Ranging
{
    public class DateTimeRange : RangeBase<DateTime>
    {
        public TimeSpan Duration => base.End - base.Start;

        public DateTimeRange(DateTime start, DateTime end) : base(start, end)
        {
        }

        public DateTimeRange(DateTime start, TimeSpan duration) : base(start, start.Add(duration))
        {
        }

        public DateTimeRange ToDateRange() => new DateTimeRange(base.Start.Date, base.End.Date);

        public IEnumerable<DateTime> Enumerate(TimespanType kind, int interval = 1)
        {
            if (kind == TimespanType.None)
            {
                throw new ArgumentException("Cannot enumerate by null");
            }

            interval = Math.Abs(interval);
            foreach (var item in base.Start.EnumerateInterval(kind, interval))
            {
                if (item > base.End)
                {
                    yield break;
                }
                yield return item;
            }
        }

        public IEnumerable<DateTimeRange> EnumerateRanges(TimespanType type, int interval = 1)
        {
            if (type == TimespanType.None)
            {
                throw new ArgumentException("Cannot enumerate by null");
            }

            var previous = base.Start;

            interval = Math.Abs(interval);
            foreach (var item in base.Start.EnumerateInterval(type, interval))
            {
                if (previous != item)
                {
                    yield return new DateTimeRange(previous, item);
                }

                previous = item;
                if (item > base.End)
                {
                    yield break;
                }
            }
        }

        public DateTimeRange GetIntersection<T>(T range)
            where T : IHasStart<DateTime>, IHasEnd<DateTime>
        {
            var start = base.Comparer.Max(base.Start, range.Start);
            var end = base.Comparer.Min(base.End, range.End);

            return new DateTimeRange(start, end);
        }
    }
}