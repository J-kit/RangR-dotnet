using RangR.Abstractions;
using RangR.Extensions;
using RangR.Maths.Subtraction;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using RangR.Maths.Absolution;

namespace RangR
{
    /*
         Comparer CheatSheet:
             -1: Left is lower than right    (a < b)
              0: Left is equal to right      (a == b)
              1: Left is bigger than right   (a > b)

             (a < b)  => Comparer.Compare(a , b) == -1
             (a > b)  => Comparer.Compare(a , b) ==  1
             (a == b) => Comparer.Compare(a , b) ==  0
     */

    [DebuggerDisplay("{" + nameof(Start) + "} - {" + nameof(End) + "} ")]
    public class RangeBase<T> : IEquatable<RangeBase<T>>, IHasStart<T>, IHasEnd<T>
    {
        private static readonly Comparer<T> DefaultComparer = Comparer<T>.Default;
        public virtual bool IsValid => Comparer.IsLessThanOrEqual(this.Start, this.End);

        public T Start { get; set; }
        public T End { get; set; }

        public Comparer<T> Comparer { get; set; }

        public RangeBase(T start, T end, Comparer<T> comparer = null)
        {
            this.Start = start;
            this.End = end;
            Comparer = comparer ?? DefaultComparer;
        }

        public bool Intersects<TComparable>(TComparable range) where TComparable : IHasStart<T>, IHasEnd<T>
        {
            return Comparer.IsLessThanOrEqual(this.Start, range.Start) && Comparer.IsBiggerThanOrEqual(this.End, range.Start) ||
                   Comparer.IsLessThanOrEqual(this.Start, range.End) && Comparer.IsBiggerThanOrEqual(this.End, range.End) ||
                   Comparer.IsBiggerThanOrEqual(this.Start, range.Start) && Comparer.IsLessThanOrEqual(this.End, range.End) ||
                   Comparer.IsLessThanOrEqual(this.Start, range.Start) && Comparer.IsBiggerThanOrEqual(this.End, range.End);

            //return this.Start.CompareTo(range.Start) <= 0 && this.End.CompareTo(range.Start) >= 0 ||
            //       this.Start.CompareTo(range.End) <= 0 && this.End.CompareTo(range.End) >= 0 ||
            //       this.Start.CompareTo(range.Start) >= 0 && this.End.CompareTo(range.End) <= 0 ||
            //       this.Start.CompareTo(range.Start) <= 0 && this.End.CompareTo(range.End) >= 0;
        }

        public bool Contains(T comparable)
        {
            return Comparer.Compare(this.Start, comparable) <= 0 && Comparer.Compare(this.End, comparable) >= 0;
        }

        public virtual bool TryMerge(T item, T maxInterval) // where T : IEquatable<T>
        {
            if (this.Contains(item))
            {
                return true;
            }

            T value;
            Action<T> setValue;
            if (this.Comparer.IsLessThan(item, this.Start))
            {
                value = this.Start;
                setValue = x => this.Start = x;
            }
            else if (this.Comparer.IsBiggerThan(item, this.End))
            {
                value = this.End;
                setValue = x => this.End = x;
            }
            else
            {
                return true;
            }

            var offsetValue = Subtractor<T>.Default.Subtract(value, item);
            var offset = Absolutor<T>.Default.Abs(offsetValue);

            if (!Comparer.IsEqualTo(offset, default(T)) && this.Comparer.IsLessThanOrEqual(offset, maxInterval))
            {
                setValue(item);
                return true;
            }
            return false;
        }

        public virtual bool TryMerge(RangeBase<T> other, T maxInterval)
        {
            var comparer = this.Comparer ?? other.Comparer;
            var subtractor = Subtractor<T>.Default;
            var absolutor = Absolutor<T>.Default;

            if (!this.IsValid || !other.IsValid)
            {
                throw new ArgumentException("Invalid range");
            }

            if (this.Intersects(other))
            {
                this.Start = comparer.Min(this.Start, other.Start);
                this.End = comparer.Max(this.End, other.End);
                return true;
            }

            var diffAbs = absolutor.Abs(subtractor.Subtract(this.Start, other.End));
            var diffAbs1 = absolutor.Abs(subtractor.Subtract(this.End, other.Start));

            if (comparer.IsBiggerThan(diffAbs, maxInterval) && comparer.IsBiggerThan(diffAbs1, maxInterval))
            {
                return false;
            }

            if (comparer.IsBiggerThanOrEqual(this.Start, other.End))
            {
                this.Start = other.Start;
                return true;
            }
            else if (comparer.IsLessThanOrEqual(this.End, other.Start))
            {
                this.End = other.End;
                return true;
            }

            return false;
        }

        public static IEnumerable<RangeBase<T>> FromEnumerable(IEnumerable<T> enumerable, T maxInterval)
        {
            var ranges = new List<RangeBase<T>>();

            RangeBase<T> currentRange = null;

            foreach (var item in enumerable)
            {
                if (currentRange == null)
                {
                    currentRange = new RangeBase<T>(item, item);
                    ranges.Add(currentRange);
                    continue;
                }

                if (currentRange.TryMerge(item, maxInterval))
                {
                    continue;
                }

                var range = ranges.FirstOrDefault(x => x != currentRange && x.TryMerge(item, maxInterval));
                if (range != null)
                {
                    currentRange = range;
                    continue;
                }

                currentRange = new RangeBase<T>(item, item);
                ranges.Add(currentRange);
            }

            var targetRanges = new List<RangeBase<T>>(ranges.Count);
            foreach (var range in ranges.Where(range => !targetRanges.Any(x => x.TryMerge(range, maxInterval))))
            {
                targetRanges.Add(range);
            }

            return targetRanges;
        }

        #region Equality Operator

        public override bool Equals(object obj) => obj is RangeBase<T> rb ? this.Equals(rb) : this.Equals(obj);

        public virtual bool Equals(RangeBase<T> other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Comparer.IsEqualTo(this.Start, other.Start) && this.Comparer.IsEqualTo(this.End, other.End);
        }

        public static bool operator ==(RangeBase<T> b1, RangeBase<T> b2)
        {
            if (b1 is null)
            {
                return b2 is null;
            }

            return b1.Equals(b2);
        }

        public static bool operator !=(RangeBase<T> b1, RangeBase<T> b2)
        {
            return !(b1 == b2);
        }

        public override int GetHashCode()
        {
            var comparer = EqualityComparer<T>.Default;
            unchecked
            {
                return (comparer.GetHashCode(Start) * 397) ^ comparer.GetHashCode(End);
            }
        }

        #endregion Equality Operator
    }
}