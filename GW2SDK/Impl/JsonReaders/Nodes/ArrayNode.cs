using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using GW2SDK.Impl.Json;
using GW2SDK.Impl.JsonReaders.Mappings;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders.Nodes
{
    public class ArrayNode : JsonNode
    {
        public JsonNode ItemNode { get; set; } = default!;

        public Type ItemType { get; set; } = typeof(object);

        public ParameterExpression ArraySeenExpr { get; set; } = default!;

        public ParameterExpression ActualValueExpr { get; set; } = default!;

        public override IEnumerable<ParameterExpression> GetVariables()
        {
            if (Mapping.Significance != MappingSignificance.Ignored)
            {
                yield return ArraySeenExpr;
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

            var arrayLengthExpr = Variable(typeof(int), "length");
            var indexExpr = Variable(typeof(int),       "index");

            var variables = new List<ParameterExpression>
            {
                arrayLengthExpr, indexExpr
            };
            variables.AddRange(ItemNode.GetVariables());

            var source = new List<Expression>
            {
                Assign(ArraySeenExpr,   Constant(true)),
                Assign(indexExpr,       Constant(0)),
                Assign(arrayLengthExpr, JsonElementExpr.GetArrayLength(jsonNodeExpr)),
                Assign(ActualValueExpr, NewArrayBounds(ItemType, arrayLengthExpr)),
                Expr.For(
                    indexExpr,
                    arrayLengthExpr,
                    (_, __) =>
                    {
                        var indexExpression = MakeIndex(jsonNodeExpr, JsonElementInfo.Item, new[] { indexExpr });
                        var indexPathExpr = JsonPathExpr.AccessArrayIndex(pathExpr, indexExpr);
                        return Block(
                            ItemNode.MapNode(indexExpression, indexPathExpr),
                            Assign(ArrayAccess(ActualValueExpr, indexExpr), ItemNode.GetResult())
                        );
                    }
                )
            };

            if (Mapping.Significance == MappingSignificance.Required)
            {
                source.Add(
                    IfThen(
                        IsFalse(ArraySeenExpr),
                        Throw(JsonExceptionExpr.Create(Constant($"Missing required value for '{Mapping.Name}'."), JsonPathExpr.ToString(pathExpr)))
                    )
                );
            }

            return IfThen(
                Equal(JsonElementExpr.GetValueKind(jsonNodeExpr), Constant(JsonValueKind.Array)),
                Block(
                    variables,
                    source
                )
            );
        }

        public override Expression GetResult() => ActualValueExpr;
    }
}
