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

        public Expression MapExpr(Expression jsonElementExpr)
        {
            if (Mapping.Significance == MappingSignificance.Ignored)
            {
                return Empty();
            }

            var arrayLengthExpr = Variable(typeof(int), "length");
            var indexExpr = Variable(typeof(int),       "index");

            return IfThen(
                Equal(JsonElementExpr.GetValueKind(jsonElementExpr), Constant(JsonValueKind.Array)),
                Block(
                    new[]
                    {
                        arrayLengthExpr,
                        indexExpr
                    },
                    Assign(ArraySeenExpr,   Constant(true)),
                    Assign(indexExpr,       Constant(0)),
                    Assign(arrayLengthExpr, Call(jsonElementExpr, JsonElementInfo.GetArrayLength)),
                    Assign(ActualValueExpr, NewArrayBounds(ItemType, arrayLengthExpr)),
                    Expr.For(
                        indexExpr,
                        arrayLengthExpr,
                        (_, __) =>
                        {
                            var indexExpression = MakeIndex(jsonElementExpr, JsonElementInfo.Item, new[] { indexExpr });
                            var valueExpr = ItemNode switch
                            {
                                ValueNode value => value.MapExpr(indexExpression),
                                ObjectNode obj => obj.MapExpr(indexExpr),
                                ArrayNode array => array.MapExpr(indexExpr),
                                _ => throw new JsonException("Mapping arrays is not yet supported for " + ItemNode.GetType())
                            };
                            return Assign(ArrayAccess(ActualValueExpr, indexExpr), valueExpr);
                        }
                    )
                )
            );
        }

        public override IEnumerable<ParameterExpression> GetVariables()
        {
            if (Mapping.Significance != MappingSignificance.Ignored)
            {
                yield return ArraySeenExpr;
                yield return ActualValueExpr;
                foreach (var child in ItemNode.GetVariables())
                {
                    yield return child;
                }
            }
        }

        public override IEnumerable<Expression> GetValidations()
        {
            if (Mapping.Significance == MappingSignificance.Required)
            {
                yield return IfThen(
                    IsFalse(ArraySeenExpr),
                    Throw(JsonExceptionExpr.Create(Constant($"Missing required value for '{Mapping.JsonPath}'.")))
                );
            }

            // TODO: item validations?
        }

        public override IEnumerable<MemberBinding> GetBindings()
        {
            yield break;
        }
    }
}
