using System;
using System.Collections.Generic;
using System.Linq;

namespace RangR
{
    public class WeekDayRange : RangeBase<DayOfWeek>
    {
        public WeekDayRange(DayOfWeek start, DayOfWeek end) : base(start, end)
        {
        }

        public static IEnumerable<WeekDayRange> FromEnumerable(IEnumerable<DayOfWeek> enumerable, int maxInterval = 1)
        {
            return IntegerRange.FromEnumerable(enumerable.Cast<int>(), maxInterval).Select(x => new WeekDayRange((DayOfWeek)x.Start, (DayOfWeek)x.End));
        }
    }
}