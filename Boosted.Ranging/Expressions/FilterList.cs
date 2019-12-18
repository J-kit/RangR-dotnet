using System;

namespace Ranging.Expressions
{
    public class FilterList<T> : ExpressionList<Func<T, bool>>
    {
    }
}