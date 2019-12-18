using System;
using System.Runtime.InteropServices.ComTypes;

namespace Ranging
{
    public enum TimespanType
    {
        None,
        Second,
        Minute,
        Hour,
        Day,
        Week,
        Month,
        Year,
    }

    public static class TimespanKindExtensions
    {
        public static Func<DateTime, int, DateTime> ToIncrementer(this TimespanType kind)
        {
            return kind switch
            {
                TimespanType.None => ((x, i) => x),

                TimespanType.Second => ((x, i) => x.AddSeconds(i)),
                TimespanType.Minute => ((x, i) => x.AddMinutes(i)),
                TimespanType.Hour => ((x, i) => x.AddHours(i)),
                TimespanType.Day => ((x, i) => x.AddDays(i)),
                TimespanType.Week => ((x, i) => x.AddDays(7 * i)),
                TimespanType.Month => ((x, i) => x.AddMonths(i)),
                TimespanType.Year => ((x, i) => x.AddYears(i)),
                _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null),
            };
        }
    }
}