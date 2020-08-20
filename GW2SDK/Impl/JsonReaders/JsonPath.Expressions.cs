using System.Linq.Expressions;
using GW2SDK.Impl.Json;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders
{
    internal static class JsonPathExpr
    {
        internal static Expression AccessProperty(Expression jsonPathExpr, Expression textExpr)
        {
            ExpressionDebug.AssertType(typeof(JsonPath), jsonPathExpr);
            ExpressionDebug.AssertType(typeof(string),   textExpr);
            return Call(jsonPathExpr, JsonPathInfo.AccessProperty, textExpr);
        }

        internal static Expression AccessArrayIndex(Expression jsonPathExpr, Expression indexExpr)
        {
            ExpressionDebug.AssertType(typeof(JsonPath), jsonPathExpr);
            ExpressionDebug.AssertType(typeof(int),      indexExpr);
            return Call(jsonPathExpr, JsonPathInfo.AccessArrayIndex, indexExpr);
        }

        internal static Expression ToString(Expression jsonPathExpr)
        {
            ExpressionDebug.AssertType(typeof(JsonPath), jsonPathExpr);
            return Call(jsonPathExpr, typeof(JsonPath).GetMethod(nameof(JsonPath.ToString)));
        }

        internal static Expression Root() => MakeMemberAccess(null, JsonPathInfo.Root);
    }
}
