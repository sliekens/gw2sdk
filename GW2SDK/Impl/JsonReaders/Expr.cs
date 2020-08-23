using System;
using System.Linq.Expressions;
using GW2SDK.Impl.Json;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders
{
    internal static class Expr
    {
        internal static Expression For(ParameterExpression indexExpr, Expression lengthExpr, DoFor bodyExpr)
        {
            var breakTarget = Label();
            var continueTarget = Label();
            return Loop(
                IfThenElse(
                    LessThan(indexExpr, lengthExpr),
                    Block(
                        bodyExpr(breakTarget, continueTarget),
                        PostIncrementAssign(indexExpr)
                    ),
                    Break(breakTarget)
                ),
                breakTarget,
                continueTarget
            );
        }

        internal static Expression UnexpectedProperty(Expression jsonPropertyExpr, Expression propertyPathExpr, Type targetType)
        {
            var format = Constant($"Unexpected property '{{0}}' at '{{1}}' for object of type '{targetType.Name}'.", typeof(string));
            var nameExpr = Property(jsonPropertyExpr, JsonPropertyInfo.Name);
            var pathExpr = JsonPathExpr.ToString(propertyPathExpr);
            return StringExpr.Format(format, nameExpr, pathExpr);
        }

        internal delegate Expression DoFor(LabelTarget @break, LabelTarget @continue);
    }
}
