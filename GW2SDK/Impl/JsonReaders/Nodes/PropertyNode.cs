using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using GW2SDK.Impl.Json;
using GW2SDK.Impl.JsonReaders.Mappings;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders.Nodes
{
    public class PropertyNode : JsonNode
    {
        public JsonNode ValueNode { get; set; } = default!;

        public ParameterExpression PropertySeenExpr { get; set; } = default!;

        public MemberInfo Destination { get; set; } = default!;

        public override IEnumerable<ParameterExpression> GetVariables()
        {
            if (Mapping.Significance != MappingSignificance.Ignored)
            {
                yield return PropertySeenExpr;
                foreach (var child in ValueNode.GetVariables())
                {
                    yield return child;
                }
            }
        }

        public Expression TestExpr(Expression jsonPropertyExpr) => JsonPropertyExpr.NameEquals(jsonPropertyExpr, Constant(Mapping.Name, typeof(string)));

        public Expression MapExpr(Expression jsonPropertyExpr, Expression objectPathExpr)
        {
            if (Mapping.Significance == MappingSignificance.Ignored)
            {
                return Empty();
            }

            var expressions = new List<Expression>();
            expressions.Add(Assign(PropertySeenExpr, Constant(true)));
            var propertyNameExpr = JsonPropertyExpr.GetName(jsonPropertyExpr);
            var propertyValueExpr = JsonPropertyExpr.GetValue(jsonPropertyExpr);
            var propertyPathExpr = JsonPathExpr.AccessProperty(objectPathExpr, propertyNameExpr);
            switch (ValueNode)
            {
                case ValueNode value:
                    expressions.Add(value.MapExpr(propertyValueExpr, propertyPathExpr));
                    break;
                case ObjectNode obj:
                    expressions.Add(obj.DeconstructExpr(propertyValueExpr, propertyPathExpr));
                    break;
                case ArrayNode array:
                    expressions.Add(array.MapExpr(propertyValueExpr, propertyPathExpr));
                    break;
                default:
                    throw new JsonException("Mapping properties is not yet supported for " + ValueNode.GetType());
            }

            return Block(expressions);
        }

        public override IEnumerable<Expression> GetValidations(Type targetType)
        {
            switch (Mapping.Significance)
            {
                case MappingSignificance.Required:
                {
                    yield return IfThen(
                        IsFalse(PropertySeenExpr),
                        Throw(JsonExceptionExpr.Create(Constant($"Missing required value for '{Mapping.Name}' for object of type '{targetType.Name}'.")))
                    );

                    foreach (var validation in ValueNode.GetValidations(targetType))
                    {
                        yield return validation;
                    }

                    break;
                }
                case MappingSignificance.Optional:
                    var childValidators = ValueNode.GetValidations(targetType).ToList();
                    if (childValidators.Count != 0)
                    {
                        yield return IfThen(
                            IsTrue(PropertySeenExpr),
                            Block(childValidators)
                        );
                    }

                    break;
            }
        }

        public override IEnumerable<MemberBinding> GetBindings()
        {
            switch (ValueNode)
            {
                case ValueNode value:
                    yield return Bind(Destination, value.ActualValueExpr);
                    break;
                case ObjectNode deconstruction when Destination is null:
                    foreach (var binding in deconstruction.Properties.SelectMany(propertyNode => propertyNode.GetBindings()))
                    {
                        yield return binding;
                    }

                    break;
                case ObjectNode obj:
                    yield return Bind(Destination, obj.CreateInstanceExpr());
                    break;

                case ArrayNode array:
                    yield return Bind(Destination, array.ActualValueExpr);
                    break;
            }
        }
    }
}
