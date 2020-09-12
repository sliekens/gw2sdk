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
        public ArrayNode(IJsonArrayMapping mapping, JsonNode itemNode)
        {
            Mapping = mapping;
            ItemNode = itemNode;
            ArraySeenExpr = Variable(typeof(bool), $"{mapping.Name}_array_seen");
            ActualValueExpr = Variable(mapping.ValueType.MakeArrayType(), $"{mapping.Name}_value");
        }

        public IJsonArrayMapping Mapping { get; }

        public JsonNode ItemNode { get; }

        public ParameterExpression ArraySeenExpr { get; }

        public ParameterExpression ActualValueExpr { get; }

        public override IEnumerable<ParameterExpression> GetVariables()
        {
            if (Mapping.Significance != MappingSignificance.Ignored)
            {
                yield return ArraySeenExpr;
                yield return ActualValueExpr;
            }
        }

        public override Expression MapNode(Expression jsonElementExpr, Expression jsonPathExpr)
        {
            ExpressionDebug.AssertType(typeof(JsonElement), jsonElementExpr);
            ExpressionDebug.AssertType(typeof(JsonPath),    jsonPathExpr);
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
                Assign(arrayLengthExpr, JsonElementExpr.GetArrayLength(jsonElementExpr)),
                Assign(ActualValueExpr, NewArrayBounds(Mapping.ValueType, arrayLengthExpr)),
                Expr.For(
                    indexExpr,
                    arrayLengthExpr,
                    (_, __) =>
                    {
                        var indexExpression = MakeIndex(jsonElementExpr, JsonElementInfo.Item, new[] { indexExpr });
                        var indexPathExpr = JsonPathExpr.AccessArrayIndex(jsonPathExpr, indexExpr);
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
                        Throw(JsonExceptionExpr.Create(Constant($"Missing required value for '{Mapping.Name}'."), JsonPathExpr.ToString(jsonPathExpr)))
                    )
                );
            }

            return IfThen(
                Equal(JsonElementExpr.GetValueKind(jsonElementExpr), Constant(JsonValueKind.Array)),
                Block(
                    variables,
                    source
                )
            );
        }

        public override Expression GetResult() => ActualValueExpr;
    }
}
