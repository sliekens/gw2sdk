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

        public MemberInfo? Destination { get; set; }

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

        public Expression MapExpr(Expression jsonPropertyExpr)
        {
            if (Mapping.Significance == MappingSignificance.Ignored)
            {
                return Empty();
            }

            var expressions = new List<Expression>();
            expressions.Add(Assign(PropertySeenExpr, Constant(true)));
            switch (ValueNode)
            {
                case ValueNode value:
                    expressions.Add(value.MapExpr(JsonPropertyExpr.GetValue(jsonPropertyExpr)));
                    break;
                case ObjectNode obj:
                    expressions.Add(obj.DeconstructExpr(JsonPropertyExpr.GetValue(jsonPropertyExpr)));
                    break;
                case DeconstructorNode deconstructor:
                    expressions.Add(deconstructor.DeconstructExpr(JsonPropertyExpr.GetValue(jsonPropertyExpr)));
                    break;
                case ArrayNode array:
                    expressions.Add(array.MapExpr(JsonPropertyExpr.GetValue(jsonPropertyExpr)));
                    break;
                default:
                    throw new JsonException("Mapping is not yet supported for " + ValueNode.GetType());
            }

            return Block(expressions);
        }

        public override IEnumerable<Expression> GetValidations()
        {
            switch (Mapping.Significance)
            {
                case MappingSignificance.Required:
                {
                    yield return IfThen(
                        IsFalse(PropertySeenExpr),
                        Throw(JsonExceptionExpr.Create(Constant($"Missing required value for '{Mapping.JsonPath}'.")))
                    );

                    foreach (var validation in ValueNode.GetValidations())
                    {
                        yield return validation;
                    }

                    break;
                }
                case MappingSignificance.Optional:
                    var childValidators = ValueNode.GetValidations().ToList();
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
                case ObjectNode obj:
                    yield return Bind(Destination, obj.CreateInstanceExpr());
                    break;

                case ArrayNode array:
                    yield return Bind(Destination, array.ActualValueExpr);
                    break;

                case DeconstructorNode deconstructor:
                    foreach (var binding in deconstructor.Children.SelectMany(propertyNode => propertyNode.GetBindings()))
                    {
                        yield return binding;
                    }

                    break;
            }
        }
    }
}
