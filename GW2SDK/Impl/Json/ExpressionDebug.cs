using System.Diagnostics;
using System.Linq.Expressions;

namespace GW2SDK.Impl.Json
{
    internal static class ExpressionDebug
    {
        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Global
        [Conditional("DEBUG")]
        internal static void AssertType<T>(Expression expression)
        {
            Debug.Assert(expression.Type == typeof(T), $"Unexpected expression type '{expression.Type.Name}', expected '{typeof(T).Name}'.");
        }
        
        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Global
        [Conditional("DEBUG")]
        internal static void AssignableTo<T>(Expression expression)
        {
            Debug.Assert(typeof(T).IsAssignableFrom(expression.Type), $"Unexpected expression type '{expression.Type.Name}', expected '{typeof(T).Name}' or a derived type.");
        }
    }
}
