using System.Collections.Generic;

namespace Ranging
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
    public class RangeBase<T> : IHasStart<T>, IHasEnd<T>
    {
        private static readonly Comparer<T> DefaultComparer = Comparer<T>.Default;

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
            return Comparer.Compare(this.Start, range.Start) <= 0 && Comparer.Compare(this.End, range.Start) >= 0 ||
                   Comparer.Compare(this.Start, range.End) <= 0 && Comparer.Compare(this.End, range.End) >= 0 ||
                   Comparer.Compare(this.Start, range.Start) >= 0 && Comparer.Compare(this.End, range.End) <= 0 ||
                   Comparer.Compare(this.Start, range.Start) <= 0 && Comparer.Compare(this.End, range.End) >= 0;

            //return this.Start.CompareTo(range.Start) <= 0 && this.End.CompareTo(range.Start) >= 0 ||
            //       this.Start.CompareTo(range.End) <= 0 && this.End.CompareTo(range.End) >= 0 ||
            //       this.Start.CompareTo(range.Start) >= 0 && this.End.CompareTo(range.End) <= 0 ||
            //       this.Start.CompareTo(range.Start) <= 0 && this.End.CompareTo(range.End) >= 0;
        }

        public bool Contains(T comparable)
        {
            Comparer.Equals(1, 1);

            return Comparer.Compare(this.Start, comparable) <= 0 && Comparer.Compare(this.End, comparable) >= 0;
        }
    }
}