using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.Json
{
    internal static class StringExpr
    {
        internal static Expression Format(Expression formatExpr, Expression arg0)
        {
            ExpressionDebug.AssertType<string>(formatExpr);
            return Call(null, StringInfo.FormatOne, formatExpr, Convert(arg0, typeof(object)));
        }

        internal static Expression Format(Expression formatExpr, Expression arg0, Expression arg1)
        {
            ExpressionDebug.AssertType<string>(formatExpr);
            return Call(null, StringInfo.FormatTwo, formatExpr, Convert(arg0, typeof(object)), Convert(arg1, typeof(object)));
        }

        internal static Expression Format(Expression formatExpr, Expression arg0, Expression arg1, Expression arg2)
        {
            ExpressionDebug.AssertType<string>(formatExpr);
            return Call(null, StringInfo.FormatThree, formatExpr, Convert(arg0, typeof(object)), Convert(arg1, typeof(object)), Convert(arg2, typeof(object)));
        }
    }
}
