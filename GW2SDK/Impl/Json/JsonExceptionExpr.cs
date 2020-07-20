using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.Json
{
    internal static class JsonExceptionExpr
    {
        internal static Expression ThrowJsonException(Expression messageExpr)
        {
            ExpressionDebug.AssertType<string>(messageExpr);
            var constructorInfo = JsonExceptionInfo.JsonExceptionConstructor;
            var exception = New(constructorInfo, messageExpr);
            return Throw(exception, exception.Type);
        }
    }
}
