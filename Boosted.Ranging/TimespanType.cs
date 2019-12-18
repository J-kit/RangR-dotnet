using System;

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
        //public static Func<DateTime, int, DateTime> ToIncrementer(this TimespanType kind)
        //{
        //    switch (kind)
        //    {
        //        //TimespanType.None => ((x, i) => x),
        //        //TimespanType.Second => ((x, i) => x.AddSeconds(i)),
        //        //TimespanType.Minute => ((x, i) => x.AddMinutes(i)),
        //        //TimespanType.Hour => ((x, i) => x.AddHours(i)),
        //        //TimespanType.Day => ((x, i) => x.AddDays(i)),
        //        //TimespanType.Week => ((x, i) => x.AddDays(7 * i)),
        //        //TimespanType.Month => ((x, i) => x.AddMonths(i)),
        //        //TimespanType.Year => ((x, i) => x.AddYears(i)),
        //        //_ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null)
        //    };
        //}

        //public static TimeSpan ToTimeSpan(this TimespanKind kind) => kind switch
        //{
        //    TimespanKind.None => TimeSpan.FromSeconds(0),
        //    TimespanKind.Second => TimeSpan.FromSeconds(1),
        //    TimespanKind.Minute => TimeSpan.FromMinutes(1),
        //    TimespanKind.Hour => TimeSpan.FromHours(1),
        //    TimespanKind.Day => TimeSpan.FromDays(1),
        //    TimespanKind.Week => TimeSpan.FromDays(7),
        //    TimespanKind.Month => TimeSpan.mont(1),
        //    TimespanKind.Year => TimeSpan.FromMinutes(1),
        //    _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null)
        //};
    }
}