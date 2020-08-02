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

        internal static Expression UnexpectedProperty(Expression jsonPathExpr, Expression jsonPropertyExpr, Type type)
        {
            var format = Expression.Constant($"Unexpected property '{{0}}.{{1}}' for object of type '{type.Name}'.", typeof(string));
            var nameExpr = Expression.Property(jsonPropertyExpr, JsonPropertyInfo.Name);
            return StringExpr.Format(format, jsonPathExpr, nameExpr);
        }

        internal delegate Expression DoFor(LabelTarget @break, LabelTarget @continue);
    }
}
