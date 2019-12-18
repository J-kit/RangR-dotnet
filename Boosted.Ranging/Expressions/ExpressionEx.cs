using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ranging.Expressions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, TReturn>> JoinExpressions<T, TReturn>(
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

    internal static class ExpressionEx
    {
        public static T ReplaceParameter<T>(T expr, ParameterExpression toReplace, ParameterExpression replacement) where T : System.Linq.Expressions.Expression
        {
            var replacer = new ExpressionReplacer(e => e == toReplace ? replacement : e);
            return (T)replacer.Visit(expr);
        }

        public static Expression<Func<T, TReturn>> Join<T, TReturn>(
            IReadOnlyCollection<Expression<Func<T, TReturn>>> expressions,
            Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, BinaryExpression> joiner
        )
        {
            if (!expressions.Any())
            {
                throw new ArgumentException("No expressions were provided");
            }
            var firstExpression = expressions.First();
            var otherExpressions = expressions.Skip(1);
            var firstParameter = firstExpression.Parameters.Single();
            var otherExpressionsWithParameterReplaced = otherExpressions.Select(e => ReplaceParameter(e.Body, e.Parameters.Single(), firstParameter));
            var bodies = new[] { firstExpression.Body }.Concat(otherExpressionsWithParameterReplaced);
            var joinedBodies = bodies.Aggregate((x, y) => joiner(x, y));
            return Expression.Lambda<Func<T, TReturn>>(joinedBodies, firstParameter);
        }

        internal class ExpressionReplacer : ExpressionVisitor
        {
            private readonly Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression> replacer;

            public ExpressionReplacer(Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression> replacer)
            {
                this.replacer = replacer;
            }

            public override System.Linq.Expressions.Expression Visit(System.Linq.Expressions.Expression node)
            {
                return base.Visit(this.replacer(node));
            }
        }
    }
}