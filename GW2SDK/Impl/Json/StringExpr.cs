using System.Linq.Expressions;

namespace GW2SDK.Impl.Json
{
    internal static class StringExpr
    {
        internal static Expression Format(Expression formatExpr, Expression arg0)
        {
            ExpressionDebug.AssertType<string>(formatExpr);
            return Expression.Call(null, StringInfo.FormatOne, formatExpr, arg0);
        }

        internal static Expression Format(Expression formatExpr, Expression arg0, Expression arg1)
        {
            ExpressionDebug.AssertType<string>(formatExpr);
            return Expression.Call(null, StringInfo.FormatTwo, formatExpr, Expression.Convert(arg0, typeof(object)), Expression.Convert(arg1, typeof(object)));
        }

        internal static Expression Format(Expression formatExpr, Expression arg0, Expression arg1, Expression arg2)
        {
            ExpressionDebug.AssertType<string>(formatExpr);
            return Expression.Call(null, StringInfo.FormatThree, formatExpr, arg0, arg1, arg2);
        }
    }
}
