using System;
using System.Linq.Expressions;
using GW2SDK.Impl.Json;

namespace GW2SDK.Impl.JsonReaders
{
    internal static class Expr
    {
        internal static Expression For(ParameterExpression indexExpr, Expression lengthExpr, DoFor bodyExpr)
        {
            var breakTarget = Expression.Label();
            var continueTarget = Expression.Label();
            return Expression.Loop(
                Expression.IfThenElse(
                    Expression.LessThan(indexExpr, lengthExpr),
                    Expression.Block(
                        bodyExpr(breakTarget, continueTarget),
                        Expression.PostIncrementAssign(indexExpr)
                    ),
                    Expression.Break(breakTarget)
                ),
                breakTarget,
                continueTarget
            );
        }

        internal static Expression UnexpectedProperty(Expression jsonPropertyExpr, Type targetType)
        {
            var format = Expression.Constant($"Unexpected property '{{0}}' for object of type '{targetType.Name}'.", typeof(string));
            var nameExpr = Expression.Property(jsonPropertyExpr, JsonPropertyInfo.Name);
            return StringExpr.Format(format, nameExpr);
        }

        internal delegate Expression DoFor(LabelTarget @break, LabelTarget @continue);
    }
}
