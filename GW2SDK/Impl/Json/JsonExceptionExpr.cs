using System.Linq.Expressions;
using GW2SDK.Impl.JsonReaders;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.Json
{
    internal static class JsonExceptionExpr
    {
        internal static Expression ThrowJsonException(Expression messageExpr, Expression pathExpr)
        {
            ExpressionDebug.AssertType<string>(messageExpr);
            ExpressionDebug.AssertType<string>(pathExpr);
            var exception = Create(messageExpr, pathExpr);
            return Throw(exception, exception.Type);
        }

        internal static NewExpression Create(Expression messageExpr, Expression pathExpr)
        {
            ExpressionDebug.AssertType<string>(messageExpr);
            ExpressionDebug.AssertType<string>(pathExpr);
            return New(JsonExceptionInfo.JsonExceptionConstructor, messageExpr, pathExpr, Default(typeof(long?)), Default(typeof(long?)));
        }
    }
}
