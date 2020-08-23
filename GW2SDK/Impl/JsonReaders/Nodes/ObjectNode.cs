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
    public class ObjectNode : JsonNode
    {
        public Type TargetType { get; set; } = typeof(object);

        public ParameterExpression ObjectSeenExpr { get; set; } = default!;

        public List<PropertyNode> Properties { get; set; } = new List<PropertyNode>();

        public UnexpectedPropertyBehavior UnexpectedPropertyBehavior { get; set; }

        public override IEnumerable<ParameterExpression> GetVariables()
        {
            if (Mapping.Significance != MappingSignificance.Ignored)
            {
                yield return ObjectSeenExpr;
                foreach (var variable in Properties.SelectMany(property => property.GetVariables()))
                {
                    yield return variable;
                }
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

            var source = new List<Expression>();

            // Important: ensure all variables are initialized to their default before mapping anything
            foreach (var variable in GetVariables())
            {
                source.Add(Assign(variable, Default(variable.Type)));
            }

            source.Add(
                IfThen(
                    Equal(JsonElementExpr.GetValueKind(jsonNodeExpr), Constant(JsonValueKind.Object)),
                    Block(
                        Assign(ObjectSeenExpr, Constant(true)),
                        JsonElementExpr.ForEachProperty(
                            jsonNodeExpr,
                            (jsonPropertyExpr, @continue) => MapPropertyExpression(jsonPropertyExpr, @continue)
                        )
                    )
                )
            );

            var propertyValidations = Properties.SelectMany(property => property.GetValidations(TargetType));
            switch (Mapping.Significance)
            {
                case MappingSignificance.Required:
                {
                    source.Add(
                        IfThen(
                            IsFalse(ObjectSeenExpr),
                            Throw(JsonExceptionExpr.Create(Constant($"Missing required value for '{Mapping.Name}' for object of type '{TargetType.Name}'.")))
                        )
                    );
                    source.AddRange(propertyValidations);

                    break;
                }
                case MappingSignificance.Optional:
                    // Only validate properties when the object is found
                    // because when the object is optional and missing, it doesn't make sense to validate its mapped properties.
                    // This means that an object can be optional and still have required properties, but they will only be validated if the object is found.
                    source.Add(
                        IfThen(
                            IsTrue(ObjectSeenExpr),
                            Block(propertyValidations.DefaultIfEmpty(Empty()))
                        )
                    );
                    break;
            }

            return Block(source);

            Expression MapPropertyExpression(Expression jsonPropertyExpr, LabelTarget @continue, int index = 0)
            {
                var propertyNameExpr = JsonPropertyExpr.GetName(jsonPropertyExpr);
                var propertyPathExpr = JsonPathExpr.AccessProperty(pathExpr, propertyNameExpr);
                if (Properties.Count == 0)
                {
                    return UnexpectedPropertyBehavior == UnexpectedPropertyBehavior.Error
                        ? JsonExceptionExpr.ThrowJsonException(Expr.UnexpectedProperty(jsonPropertyExpr, propertyPathExpr, TargetType))
                        : Continue(@continue);
                }

                var propertyNode = Properties[index];
                return IfThenElse(
                    propertyNode.TestExpr(jsonPropertyExpr),
                    propertyNode.MapNode(jsonPropertyExpr, propertyPathExpr),
                    index + 1 < Properties.Count
                        ? MapPropertyExpression(jsonPropertyExpr, @continue, index + 1)
                        : UnexpectedPropertyBehavior == UnexpectedPropertyBehavior.Error
                            ? JsonExceptionExpr.ThrowJsonException(Expr.UnexpectedProperty(jsonPropertyExpr, propertyPathExpr, TargetType))
                            : Continue(@continue)
                );
            }
        }

        public override Expression GetResult() =>
            MemberInit(
                New(TargetType),
                Properties.SelectMany(property => property.GetBindings())
            );
    }
}
