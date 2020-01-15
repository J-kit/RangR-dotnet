using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RangR.Expressions
{
    public static class ExpressionExtensions
    {
        internal static Expression<Func<T, TReturn>> JoinExpressions<T, TReturn>(
            this IEnumerable<Expression<Func<T, TReturn>>> expressions,
            Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, BinaryExpression> joinCondition
        )
        {
            return ExpressionEx.Join(expressions.ToList(), joinCondition);
        }

        public static RangeExpressionBuilder<TValue> ExpressionBuilder<TValue>(this RangeBase<TValue> range)
            where TValue : IComparable<TValue>
        {
            return new RangeExpressionBuilder<TValue>(range);
        }
    }
}