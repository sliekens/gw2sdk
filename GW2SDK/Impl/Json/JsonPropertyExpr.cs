using System.Linq.Expressions;
using System.Text.Json;
using static System.Linq.Expressions.Expression;
using static GW2SDK.Impl.Json.ExpressionDebug;

namespace GW2SDK.Impl.Json
{
    internal static class JsonPropertyExpr
    {
        internal static Expression GetName(Expression jsonPropertyExpr)
        {
            AssertType<JsonProperty>(jsonPropertyExpr);
            return Property(jsonPropertyExpr, JsonPropertyInfo.Name);
        }

        internal static Expression GetValue(Expression jsonPropertyExpr)
        {
            AssertType<JsonProperty>(jsonPropertyExpr);
            return Property(jsonPropertyExpr, JsonPropertyInfo.Value);
        }

        internal static Expression NameEquals(Expression jsonPropertyExpr, Expression textExpr)
        {
            AssertType<JsonProperty>(jsonPropertyExpr);
            AssertType<string>(textExpr);
            return Call(jsonPropertyExpr, JsonPropertyInfo.NameEquals, textExpr);
        }
    }
}
