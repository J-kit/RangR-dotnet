using System;
using System.Collections.Generic;
using System.Diagnostics;
using RangR.Abstractions;
using RangR.Extensions;

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