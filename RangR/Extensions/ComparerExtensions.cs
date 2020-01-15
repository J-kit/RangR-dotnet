using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace RangR.Extensions
{
    public static class ComparerExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBiggerThan<T>(this IComparer<T> comparer, T value1, T value2)
        {
            return comparer.Compare(value1, value2) == 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessThan<T>(this IComparer<T> comparer, T value1, T value2)
        {
            return comparer.Compare(value1, value2) == -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo<T>(this IComparer<T> comparer, T value1, T value2)
        {
            return comparer.Compare(value1, value2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBiggerThanOrEqual<T>(this IComparer<T> comparer, T value1, T value2)
        {
            return comparer.Compare(value1, value2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessThanOrEqual<T>(this IComparer<T> comparer, T value1, T value2)
        {
            return comparer.Compare(value1, value2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(this IComparer<T> comparer, T first, T second)
        {
            return comparer.Compare(first, second) > 0 ? first : second;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(this IComparer<T> comparer, T first, T second)
        {
            return comparer.Compare(first, second) < 0 ? first : second;
        }
    }
}