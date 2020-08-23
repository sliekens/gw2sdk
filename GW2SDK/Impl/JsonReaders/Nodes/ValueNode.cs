using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using GW2SDK.Impl.Json;
using GW2SDK.Impl.JsonReaders.Mappings;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders.Nodes
{
    public class ValueNode : JsonNode
    {
        public ParameterExpression ActualValueExpr { get; set; } = default!;

        public IValueExpressionMapper ValueExpressionMapper { get; set; } = default!;

        public override IEnumerable<ParameterExpression> GetVariables()
        {
            if (Mapping.Significance != MappingSignificance.Ignored)
            {
                yield return ActualValueExpr;
            }
        }

        public override Expression MapNode(Expression jsonNodeExpr, Expression pathExpr)
        {
            ExpressionDebug.AssertType(typeof(JsonElement), jsonNodeExpr);
            ExpressionDebug.AssertType(typeof(JsonPath),    pathExpr);
            if (Mapping.Significance == MappingSignificance.Ignored)
            {
                return Empty();
            }

            var mapValueExpr = ValueExpressionMapper.MapValueExpr(jsonNodeExpr, pathExpr);
            return Assign(ActualValueExpr, mapValueExpr);
        }

        public override Expression GetResult() => ActualValueExpr;
    }
}
