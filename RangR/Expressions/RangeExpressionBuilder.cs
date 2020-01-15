using System;
using System.Linq;
using System.Linq.Expressions;

namespace RangR.Expressions
{
    public class RangeExpressionBuilder<TValue> where TValue : IComparable<TValue>
    {
        private readonly RangeBase<TValue> range;

        public RangeExpressionBuilder(RangeBase<TValue> range)
        {
            this.range = range;
        }

        /// <summary>
        /// Warning! Borderline code
        /// </summary>
        /// <typeparam name="TDomainEntity"></typeparam>
        /// <param name="startExpression"></param>
        /// <param name="endExpression"></param>
        /// <returns></returns>
        public Expression<Func<TDomainEntity, bool>> BuildIntersectionExpression<TDomainEntity>
        (
            Expression<Func<TDomainEntity, TValue>> startExpression,
            Expression<Func<TDomainEntity, TValue>> endExpression
        )
        {
            var expressionBuilder = new DynamicExpressionBuilder<TDomainEntity>();

            var startProperty = ExpressionEx.ReplaceParameter(startExpression.Body, startExpression.Parameters.Single(), expressionBuilder.Parameter);
            var endProperty = ExpressionEx.ReplaceParameter(endExpression.Body, endExpression.Parameters.Single(), expressionBuilder.Parameter);

            //We could also use const like below, but that would make our query more uglier
            //var startConst = Expression.Constant(this.Start);
            //var endConst = Expression.Constant(this.End);

            var startConst = Expression.Property(Expression.Constant(this.range, typeof(RangeBase<TValue>)), nameof(this.range.Start));
            var endConst = Expression.Property(Expression.Constant(this.range, typeof(RangeBase<TValue>)), nameof(this.range.End));

            return new[]
            {
                new []
                {
                    expressionBuilder.BuildFragment(ExpressionType.LessThanOrEqual, startProperty, startConst),
                    expressionBuilder.BuildFragment(ExpressionType.GreaterThanOrEqual, endProperty, startConst)
                },
                new []
                {
                    expressionBuilder.BuildFragment(ExpressionType.LessThanOrEqual, startProperty, endConst),
                    expressionBuilder.BuildFragment(ExpressionType.GreaterThanOrEqual, endProperty, endConst)
                },
                new []
                {
                    expressionBuilder.BuildFragment(ExpressionType.GreaterThanOrEqual, startProperty, startConst),
                    expressionBuilder.BuildFragment(ExpressionType.LessThanOrEqual, endProperty, endConst),
                },
                new []
                {
                    expressionBuilder.BuildFragment(ExpressionType.LessThanOrEqual, startProperty, startConst),
                    expressionBuilder.BuildFragment(ExpressionType.GreaterThanOrEqual, endProperty, endConst)
                }
            }
            .Select(x => x.JoinExpressions(Expression.AndAlso))
            .JoinExpressions(Expression.Or);
        }
    }
}