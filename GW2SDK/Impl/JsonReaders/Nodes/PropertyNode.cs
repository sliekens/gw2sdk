using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using GW2SDK.Impl.Json;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Impl.JsonReaders.Nodes
{
    public class PropertyNode : JsonNode
    {
        public JsonNode ValueNode { get; set; } = default!;

        public override IEnumerable<ParameterExpression> GetVariables()
        {
            if (Mapping.Significance != MappingSignificance.Ignored)
            {
                yield return ValueSeenExpr;
                foreach (var child in ValueNode.GetVariables())
                {
                    yield return child;
                }
            }
        }

        public Expression TestExpr(Expression jsonPropertyExpr) => JsonPropertyExpr.NameEquals(jsonPropertyExpr, Expression.Constant(Mapping.Name, typeof(string)));

        public Expression MapExpr(Expression jsonPropertyExpr)
        {
            if (Mapping.Significance == MappingSignificance.Ignored)
            {
                return Expression.Empty();
            }

            var expressions = new List<Expression>();
            expressions.Add(Expression.Assign(ValueSeenExpr, Expression.Constant(true)));
            switch (ValueNode)
            {
                case LeafNode leaf:
                    expressions.Add(leaf.MapExpr(JsonPropertyExpr.GetValue(jsonPropertyExpr)));
                    break;
                case ObjectNode obj:
                    expressions.Add(obj.MapExpr(JsonPropertyExpr.GetValue(jsonPropertyExpr)));
                    break;
                default:
                    throw new JsonException("Mapping is not yet supported for " + ValueNode.GetType());
            }

            return Expression.Block(expressions);
        }

        public override IEnumerable<Expression> GetValidations()
        {
            switch (Mapping.Significance)
            {
                case MappingSignificance.Required:
                {
                    yield return Expression.IfThen(
                        Expression.IsFalse(ValueSeenExpr),
                        Expression.Throw(JsonExceptionExpr.Create(Expression.Constant($"Missing required value for '{Mapping.JsonPath}'.")))
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
                        yield return Expression.IfThen(
                            Expression.IsTrue(ValueSeenExpr),
                            Expression.Block(childValidators)
                        );
                    }
                    break;
            }
        }

        public override IEnumerable<MemberBinding> GetBindings()
        {
            switch (ValueNode)
            {
                case LeafNode leaf:
                    yield return Expression.Bind(Mapping.Destination, leaf.ActualValueExpr);
                    break;
                case ObjectNode obj:
                    foreach (var binding in obj.Children.SelectMany(propertyNode => propertyNode.GetBindings()))
                    {
                        yield return binding;
                    }

                    break;
            }
        }
    }
}