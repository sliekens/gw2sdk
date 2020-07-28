using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.Json
{
    internal static class JsonExceptionExpr
    {
        internal static Expression ThrowJsonException(Expression messageExpr)
        {
            ExpressionDebug.AssertType<string>(messageExpr);
            var exception = Create(messageExpr);
            return Throw(exception, exception.Type);
        }

        internal static NewExpression Create(Expression messageExpr)
        {
            ExpressionDebug.AssertType<string>(messageExpr);
            return New(JsonExceptionInfo.JsonExceptionConstructor, messageExpr);
        }
    }
}
