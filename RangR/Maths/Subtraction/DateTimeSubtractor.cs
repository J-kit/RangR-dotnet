using System;

namespace RangR.Maths.Subtraction
{
    public class DatetimeSubtractor : Subtractor<DateTime>
    {
        public override DateTime Subtract(DateTime v1, DateTime v2)
        {
            return DateTime.MinValue.Add(v1 - v2);
        }
    }
}