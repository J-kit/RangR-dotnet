using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ranging
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