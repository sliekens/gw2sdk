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

        public Expression DeconstructExpr(Expression jsonElementExpr)
        {
            if (Mapping.Significance == MappingSignificance.Ignored)
            {
                return Empty();
            }

            return IfThen(
                Equal(JsonElementExpr.GetValueKind(jsonElementExpr), Constant(JsonValueKind.Object)),
                Block(
                    Assign(ObjectSeenExpr, Constant(true)),
                    JsonElementExpr.ForEachProperty(jsonElementExpr, (jsonPropertyExpr, @continue) => MapPropertyExpression(jsonPropertyExpr, @continue))
                )
            );

            Expression MapPropertyExpression(Expression jsonPropertyExpr, LabelTarget @continue, int index = 0)
            {
                if (Properties.Count == 0)
                {
                    return UnexpectedPropertyBehavior == UnexpectedPropertyBehavior.Error
                        ? JsonExceptionExpr.ThrowJsonException(Expr.UnexpectedProperty(jsonPropertyExpr, TargetType))
                        : Continue(@continue);
                }

                var child = Properties[index];
                return IfThenElse(
                    child.TestExpr(jsonPropertyExpr),
                    child.MapExpr(jsonPropertyExpr),
                    index + 1 < Properties.Count
                        ? MapPropertyExpression(jsonPropertyExpr, @continue, index + 1)
                        : UnexpectedPropertyBehavior == UnexpectedPropertyBehavior.Error
                            ? JsonExceptionExpr.ThrowJsonException(Expr.UnexpectedProperty(jsonPropertyExpr, TargetType))
                            : Continue(@continue)
                );
            }
        }

        public Expression MapExpr(Expression jsonElementExpr)
        {
            if (Mapping.Significance == MappingSignificance.Ignored)
            {
                return Empty();
            }

            var source = new List<Expression>();
            source.Add(
                IfThen(
                    Equal(JsonElementExpr.GetValueKind(jsonElementExpr), Constant(JsonValueKind.Object)),
                    Block(
                        Assign(ObjectSeenExpr, Constant(true)),
                        JsonElementExpr.ForEachProperty(
                            jsonElementExpr,
                            (jsonPropertyExpr, @continue) => MapPropertyExpression(jsonPropertyExpr, @continue)
                        )
                    )
                )
            );
            source.AddRange(GetValidations(TargetType));
            source.Add(CreateInstanceExpr());

            return Block(
                GetVariables(),
                source
            );

            Expression MapPropertyExpression(Expression jsonPropertyExpr, LabelTarget @continue, int index = 0)
            {
                if (Properties.Count == 0)
                {
                    return UnexpectedPropertyBehavior == UnexpectedPropertyBehavior.Error
                        ? JsonExceptionExpr.ThrowJsonException(Expr.UnexpectedProperty(jsonPropertyExpr, TargetType))
                        : Continue(@continue);
                }

                var child = Properties[index];
                return IfThenElse(
                    child.TestExpr(jsonPropertyExpr),
                    child.MapExpr(jsonPropertyExpr),
                    index + 1 < Properties.Count
                        ? MapPropertyExpression(jsonPropertyExpr, @continue, index + 1)
                        : UnexpectedPropertyBehavior == UnexpectedPropertyBehavior.Error
                            ? JsonExceptionExpr.ThrowJsonException(Expr.UnexpectedProperty(jsonPropertyExpr, TargetType))
                            : Continue(@continue)
                );
            }
        }

        public Expression CreateInstanceExpr() =>
            MemberInit(
                New(TargetType),
                Properties.SelectMany(child => child.GetBindings())
            );

        public override IEnumerable<ParameterExpression> GetVariables()
        {
            if (Mapping.Significance != MappingSignificance.Ignored)
            {
                yield return ObjectSeenExpr;
                foreach (var child in Properties)
                {
                    foreach (var variable in child.GetVariables())
                    {
                        yield return variable;
                    }
                }
            }
        }

        public override IEnumerable<Expression> GetValidations(Type targetType)
        {
            switch (Mapping.Significance)
            {
                case MappingSignificance.Required:
                {
                    yield return IfThen(
                        IsFalse(ObjectSeenExpr),
                        Throw(JsonExceptionExpr.Create(Constant($"Missing required value for '{Mapping.Name}'.")))
                    );
                    foreach (var validation in Properties.SelectMany(child => child.GetValidations(TargetType)))
                    {
                        yield return validation;
                    }

                    break;
                }
                case MappingSignificance.Optional:
                    var childValidators = Properties.SelectMany(child => child.GetValidations(TargetType)).ToList();
                    if (childValidators.Count != 0)
                    {
                        yield return IfThen(
                            IsTrue(ObjectSeenExpr),
                            Block(childValidators)
                        );
                    }

                    break;
            }
        }

        public override IEnumerable<MemberBinding> GetBindings()
        {
            yield break;
        }
    }
}
