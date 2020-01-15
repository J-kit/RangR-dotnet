using System;
using System.Collections.Generic;
using System.Linq;

namespace RangR.Extensions
{
    public static class DatetimeExtensions
    {
        public static DateTime NextWeekDay(this DateTime currentDateTime, DayOfWeek dayOfWeek)
        {
            while (currentDateTime.DayOfWeek != dayOfWeek)
            {
                currentDateTime = currentDateTime.AddDays(1);
            }

            return currentDateTime;
        }

        public static DateTime FirstDayOfMonth(this DateTime dateTime)
        {
            var date = dateTime.Date;

            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime LastDayOfMonth(this DateTime dateTime)
        {
            return dateTime.FirstDayOfMonth().AddMonths(1).AddDays(-1);
        }

        public static DateTime EndOfDay(this DateTime dateTime)
        {
            return dateTime.Date.AddDays(1).AddSeconds(-1);
        }

        public static IEnumerable<DateTime> EnumerateInterval(this DateTime dateTime, TimespanType kind, int interval = 1)
        {
            if (kind == TimespanType.None)
            {
                throw new ArgumentException("Cannot enumerate by null");
            }

            var incrementer = kind.ToIncrementer();

            for (var dt = dateTime; ; dt = incrementer(dt, interval))
            {
                yield return dt;
            }
        }

        public static int GetMonthlyOffset(this DateTime day)
        {
            return new DateTimeRange(day.FirstDayOfMonth(), day).Enumerate(TimespanType.Day).Count(x => x.DayOfWeek == day.DayOfWeek);
        }
    }
}