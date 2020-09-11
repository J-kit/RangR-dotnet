using RangR.Abstractions;

using System.Collections.Generic;

namespace RangR.Extensions
{
    public static class RangeExtensions
    {
        public static bool Intersects<T>(this RangeBase<T> range, T v1, T v2)
        {
            return range.Intersects(new RangeBase<T>(v1, v2));
        }

        public static bool RangeIsValid<TRange, TValue>(this TRange range, IComparer<TValue> comparer = null) where TRange : IHasStart<TValue>, IHasEnd<TValue>
        {
            if (range is ICanBeValid canBeValid)
            {
                return canBeValid.IsValid;
            }

            return (comparer ?? Comparer<TValue>.Default).IsLessThanOrEqual(range.Start, range.End);
        }
    }
}