using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using GW2SDK.Impl.Json;
using GW2SDK.Impl.JsonReaders.Mappings;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders.Nodes
{
    public class PropertyNode : JsonNode
    {
        public PropertyNode(IJsonPropertyMapping mapping, JsonNode? valueNode)
        {
            Mapping = mapping;
            ValueNode = valueNode;
            PropertySeenExpr = Variable(typeof(bool), $"{mapping.Name}_key_seen");
        }

        public IJsonPropertyMapping Mapping { get; }

        public JsonNode? ValueNode { get; }

        public ParameterExpression PropertySeenExpr { get; }

        public override IEnumerable<ParameterExpression> GetVariables()
        {
            if (Mapping.Significance != MappingSignificance.Ignored)
            {
                yield return PropertySeenExpr;
                foreach (var child in ValueNode!.GetVariables())
                {
                    yield return child;
                }
            }
        }

        public Expression TestExpr
            (Expression jsonPropertyExpr) =>
            JsonPropertyExpr.NameEquals(jsonPropertyExpr, Constant(Mapping.Name, typeof(string)));

        public IEnumerable<Expression> GetValidations(Type targetType, Expression pathExpr)
        {
            ExpressionDebug.AssertType(typeof(JsonPath), pathExpr);
            if (Mapping.Significance == MappingSignificance.Required)
            {
                yield return IfThen
                (
                    IsFalse(PropertySeenExpr),
                    Throw
                    (
                        JsonExceptionExpr.Create
                        (
                            Constant
                            (
                                $"Missing required value for '{Mapping.Name}' for object of type '{targetType.Name}'."
                            ),
                            JsonPathExpr.ToString(pathExpr)
                        )
                    )
                );
            }
        }

        public IEnumerable<MemberBinding> GetBindings()
        {
            switch (ValueNode)
            {
                case ValueNode value:
                    yield return Bind(Mapping.Destination, value.GetResult());
                    break;
                case ObjectNode deconstruction when Mapping.Destination is null:
                    foreach (var binding in deconstruction.Properties.SelectMany
                        (propertyNode => propertyNode.GetBindings()))
                    {
                        yield return binding;
                    }

                    break;
                case ObjectNode obj:
                    yield return Bind(Mapping.Destination, obj.GetResult());
                    break;

                case ArrayNode array:
                    yield return Bind(Mapping.Destination, array.GetResult());
                    break;
            }
        }

        public override Expression MapNode(Expression jsonElementExpr, Expression jsonPathExpr)
        {
            ExpressionDebug.AssertType(typeof(JsonProperty), jsonElementExpr);
            ExpressionDebug.AssertType(typeof(JsonPath), jsonPathExpr);
            if (Mapping.Significance == MappingSignificance.Ignored)
            {
                return Empty();
            }

            var expressions = new List<Expression>();
            expressions.Add(Assign(PropertySeenExpr, Constant(true)));
            ;
            var propertyValueExpr = JsonPropertyExpr.GetValue(jsonElementExpr);
            switch (ValueNode)
            {
                case ValueNode value:
                    expressions.Add(value.MapNode(propertyValueExpr, jsonPathExpr));
                    break;
                case ObjectNode obj:
                    expressions.Add(obj.MapNode(propertyValueExpr, jsonPathExpr));
                    break;
                case ArrayNode array:
                    expressions.Add(array.MapNode(propertyValueExpr, jsonPathExpr));
                    break;
                default:
                    throw new JsonException
                        ("Mapping properties is not yet supported for " + ValueNode!.GetType());
            }

            return Block(expressions);
        }

        public override Expression GetResult() => ValueNode!.GetResult();
    }
}
