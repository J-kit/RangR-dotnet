using System;

namespace RangR
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
                TimespanType.None =>   new Func<DateTime, int, DateTime>((x, i) => x),
                TimespanType.Second => new Func<DateTime, int, DateTime>((x, i) => x.AddSeconds(i)),
                TimespanType.Minute => new Func<DateTime, int, DateTime>((x, i) => x.AddMinutes(i)),
                TimespanType.Hour =>   new Func<DateTime, int, DateTime>((x, i) => x.AddHours(i)),
                TimespanType.Day =>    new Func<DateTime, int, DateTime>((x, i) => x.AddDays(i)),
                TimespanType.Week =>   new Func<DateTime, int, DateTime>((x, i) => x.AddDays(7 * i)),
                TimespanType.Month =>  new Func<DateTime, int, DateTime>((x, i) => x.AddMonths(i)),
                TimespanType.Year =>   new Func<DateTime, int, DateTime>((x, i) => x.AddYears(i)),
                _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null),
            };
        }
    }
}