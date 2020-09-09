using System;
using RangR.Abstractions;

namespace RangR.Extensions
{
    public static class DateTimeRangeExtensions
    {
        public static DateTimeRange AsDateTimeRange<T>(this T source) where T : IHasStart<DateTime>, IHasEnd<DateTime>
        {
            return new DateTimeRange(source.Start, source.End);
        }

        public static TimeSpan Duration<T>(this T source) where T : IHasStart<DateTime>, IHasEnd<DateTime>
        {
            return source.AsDateTimeRange().Duration;
        }
    }
}