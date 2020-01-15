using System;
using System.Linq.Expressions;

namespace RangR.Expressions
{
    internal class DynamicExpressionBuilder<TDomainEntity>
    {
        public ParameterExpression Parameter { get; private set; }

        public DynamicExpressionBuilder()
        {
            this.Parameter = System.Linq.Expressions.Expression.Parameter(typeof(TDomainEntity), "x");
        }

        public Expression<Func<TDomainEntity, bool>> BuildFragment(ExpressionType nodeType, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right)
        {
            var expr = nodeType switch
            {
                ExpressionType.GreaterThanOrEqual => System.Linq.Expressions.Expression.GreaterThanOrEqual(left, right),
                ExpressionType.LessThanOrEqual => System.Linq.Expressions.Expression.LessThanOrEqual(left, right),
                _ => null
            };

            if (expr == null)
            {
                return null;
            }

            return System.Linq.Expressions.Expression.Lambda<Func<TDomainEntity, bool>>(expr, this.Parameter);
        }
    }
}