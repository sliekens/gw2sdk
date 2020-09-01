using System.Linq.Expressions;
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

        internal delegate Expression DoFor(LabelTarget @break, LabelTarget @continue);
    }
}
