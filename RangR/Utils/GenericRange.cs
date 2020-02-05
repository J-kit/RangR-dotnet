using RangR.Extensions;
using RangR.Maths.Subtraction;

using System;
using RangR.Maths.Absolution;

namespace RangR.Utils
{
    internal static class GenericRange
    {
        public static bool TryMerge<T>(this RangeBase<T> rangeBase, T item, T maxInterval) where T : IEquatable<T>
        {
            if (rangeBase.Contains(item))
            {
                return true;
            }

            T value;
            Action<T> setValue;
            if (rangeBase.Comparer.IsLessThan(item, rangeBase.Start))
            {
                value = rangeBase.Start;
                setValue = x => rangeBase.Start = x;
            }
            else if (rangeBase.Comparer.IsBiggerThan(item, rangeBase.End))
            {
                value = rangeBase.End;
                setValue = x => rangeBase.End = x;
            }
            else
            {
                return true;
            }

            var offsetValue = Subtractor<T>.Default.Subtract(value, item);
            var offset = Absolutor<T>.Default.Abs(offsetValue);

            if (!offset.Equals(default(T)) && rangeBase.Comparer.IsLessThanOrEqual(offset, maxInterval))
            {
                setValue(item);
                return true;
            }
            return false;
        }

        public static bool TryMerge<T>(this RangeBase<T> rangeBase, RangeBase<T> other, T maxInterval)
        {
            var comparer = rangeBase.Comparer ?? other.Comparer;
            if (!rangeBase.IsValid || !other.IsValid)
            {
                throw new ArgumentException("Invalid range");
            }

            if (rangeBase.Intersects(other))
            {
                rangeBase.Start = comparer.Min(rangeBase.Start, other.Start);
                rangeBase.End = comparer.Max(rangeBase.End, other.End);
                return true;
            }

            var rangeStartDiff = Subtractor<T>.Default.Subtract(rangeBase.Start, other.End);
            var diffAbs = Absolutor<T>.Default.Abs(rangeStartDiff);
            if (comparer.IsBiggerThan(diffAbs, maxInterval))
            {
                return false;
            }

            if (comparer.IsBiggerThanOrEqual(rangeBase.Start, other.End))
            {
                rangeBase.Start = other.Start;
                return true;
            }
            else if (comparer.IsLessThanOrEqual(rangeBase.End, other.Start))
            {
                rangeBase.End = other.End;
                return true;
            }

            return false;
        }
    }
}