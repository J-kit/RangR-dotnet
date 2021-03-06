﻿using RangR.Abstractions;
using RangR.Exceptions;
using RangR.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;

namespace RangR
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
            if (!this.Intersects(range))
            {
                throw new OutOfRangeException("Ranges don't intersect!");
            }

            var start = base.Comparer.Max(base.Start, range.Start);
            var end = base.Comparer.Min(base.End, range.End);

            return new DateTimeRange(start, end);
        }


        public static IEnumerable<DateTimeRange> FromEnumerable(IEnumerable<DateTime> dates, TimeSpan maxInterval)
        {
            return RangeBase<long>.FromEnumerable
            (
                dates.Select(x => x.ToFileTimeUtc()),
                (long) maxInterval.Ticks //TimeSpan.FromHours(0.5).Ticks
            ).Select(x => new DateTimeRange(DateTime.FromFileTimeUtc(x.Start), DateTime.FromFileTimeUtc(x.End)));
        }
    }
}