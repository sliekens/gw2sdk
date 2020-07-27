using System.Linq.Expressions;
using System.Text.Json;

namespace GW2SDK.Impl.Json
{
    internal static class JsonPropertyExpr
    {
        internal static Expression GetName(Expression jsonPropertyExpression)
        {
            ExpressionDebug.AssertType<JsonProperty>(jsonPropertyExpression);
            return Expression.Property(jsonPropertyExpression, JsonPropertyInfo.Name);
        }

        internal static Expression GetValue(Expression jsonPropertyExpression)
        {
            ExpressionDebug.AssertType<JsonProperty>(jsonPropertyExpression);
            return Expression.Property(jsonPropertyExpression, JsonPropertyInfo.Value);
        }

        internal static Expression NameEquals(Expression jsonPropertyExpression, Expression textExpression)
        {
            ExpressionDebug.AssertType<JsonProperty>(jsonPropertyExpression);
            ExpressionDebug.AssertType<string>(textExpression);
            return Expression.Call(jsonPropertyExpression, JsonPropertyInfo.NameEquals, textExpression);
        }
    }
}
