using System;
using System.Collections.Generic;
using System.Text;

namespace Ranging.Extensions
{
    public static class ComparerExtensions
    {
        public static bool IsBiggerThan<T>(this IComparer<T> comparer, T value1, T value2)
        {
            return comparer.Compare(value1, value2) == 1;
        }

        public static bool IsLessThan<T>(this IComparer<T> comparer, T value1, T value2)
        {
            return comparer.Compare(value1, value2) == -1;
        }

        public static bool IsEqualTo<T>(this IComparer<T> comparer, T value1, T value2)
        {
            return comparer.Compare(value1, value2) == 0;
        }


        public static bool IsBiggerThanOrEqual<T>(this IComparer<T> comparer, T value1, T value2)
        {
            return comparer.Compare(value1, value2) >= 0;
        }

        public static bool IsLessThanOrEqual<T>(this IComparer<T> comparer, T value1, T value2)
        {
            return comparer.Compare(value1, value2) >= 0;
        }

    }
}

