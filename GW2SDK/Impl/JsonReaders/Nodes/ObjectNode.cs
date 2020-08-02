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
        public Type ObjectType { get; set; } = typeof(object);

        public ParameterExpression ObjectSeenExpr { get; set; } = default!;

        public List<PropertyNode> Children { get; set; } = new List<PropertyNode>();

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
                if (Children.Count == 0)
                {
                    return UnexpectedPropertyBehavior == UnexpectedPropertyBehavior.Error
                        ? JsonExceptionExpr.ThrowJsonException(Expr.UnexpectedProperty(Constant(Mapping.Name, typeof(string)), jsonPropertyExpr, ObjectType))
                        : Continue(@continue);
                }

                var child = Children[index];
                return IfThenElse(
                    child.TestExpr(jsonPropertyExpr),
                    child.MapExpr(jsonPropertyExpr),
                    index + 1 < Children.Count
                        ? MapPropertyExpression(jsonPropertyExpr, @continue, index + 1)
                        : UnexpectedPropertyBehavior == UnexpectedPropertyBehavior.Error
                            ? JsonExceptionExpr.ThrowJsonException(Expr.UnexpectedProperty(Constant(Mapping.Name, typeof(string)), jsonPropertyExpr, ObjectType))
                            : Continue(@continue)
                );
            }
        }

        // TODO: deduplicate code
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
            source.AddRange(GetValidations(ObjectType));
            source.Add(CreateInstanceExpr());

            return Block(
                GetVariables(),
                source
            );

            Expression MapPropertyExpression(Expression jsonPropertyExpr, LabelTarget @continue, int index = 0)
            {
                if (Children.Count == 0)
                {
                    return UnexpectedPropertyBehavior == UnexpectedPropertyBehavior.Error
                        ? JsonExceptionExpr.ThrowJsonException(Expr.UnexpectedProperty(Constant(Mapping.Name, typeof(string)), jsonPropertyExpr, ObjectType))
                        : Continue(@continue);
                }

                var child = Children[index];
                return IfThenElse(
                    child.TestExpr(jsonPropertyExpr),
                    child.MapExpr(jsonPropertyExpr),
                    index + 1 < Children.Count
                        ? MapPropertyExpression(jsonPropertyExpr, @continue, index + 1)
                        : UnexpectedPropertyBehavior == UnexpectedPropertyBehavior.Error
                            ? JsonExceptionExpr.ThrowJsonException(Expr.UnexpectedProperty(Constant(Mapping.Name, typeof(string)), jsonPropertyExpr, ObjectType))
                            : Continue(@continue)
                );
            }
        }

        public Expression CreateInstanceExpr() =>
            MemberInit(
                New(ObjectType),
                GetBindings()
            );

        public override IEnumerable<ParameterExpression> GetVariables()
        {
            if (Mapping.Significance != MappingSignificance.Ignored)
            {
                yield return ObjectSeenExpr;
                foreach (var child in Children)
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
                    foreach (var validation in Children.SelectMany(child => child.GetValidations(ObjectType)))
                    {
                        yield return validation;
                    }

                    break;
                }
                case MappingSignificance.Optional:
                    var childValidators = Children.SelectMany(child => child.GetValidations(ObjectType)).ToList();
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
            if (Mapping.Significance == MappingSignificance.Ignored)
            {
                yield break;
            }

            foreach (var binding in Children.SelectMany(child => child.GetBindings()))
            {
                yield return binding;
            }
        }
    }
}
