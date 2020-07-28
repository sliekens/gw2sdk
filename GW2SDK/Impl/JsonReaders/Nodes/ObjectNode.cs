﻿using System;
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

        public ParameterExpression ActualValueExpr { get; set; } = default!;

        public List<PropertyNode> Children { get; set; } = new List<PropertyNode>();

        public UnexpectedPropertyBehavior UnexpectedPropertyBehavior { get; set; }

        public ParameterExpression ObjectSeenExpr { get; set; } = default!;

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
                    Children.Count == 0
                        ? Empty()
                        : JsonElementExpr.ForEachProperty(jsonElementExpr, (jsonPropertyExpr, @continue) => MapPropertyExpression(jsonPropertyExpr, @continue))
                )
            );

            Expression MapPropertyExpression(Expression jsonPropertyExpr, LabelTarget @continue, int index = 0)
            {
                var child = Children[index];
                return IfThenElse(
                    child.TestExpr(jsonPropertyExpr),
                    child.MapExpr(jsonPropertyExpr),
                    index + 1 < Children.Count
                        ? MapPropertyExpression(jsonPropertyExpr, @continue, index + 1)
                        : UnexpectedPropertyBehavior == UnexpectedPropertyBehavior.Error
                            ? Expr.ThrowJsonException(Expr.UnexpectedProperty(Constant(Mapping.JsonPath, typeof(string)), jsonPropertyExpr))
                            : Continue(@continue)
                );
            }
        }

        public Expression MapExpr(Expression jsonElementExpr) => Assign(ActualValueExpr, CreateExpr(jsonElementExpr));

        public Expression CreateExpr(Expression jsonElementExpr)
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
                        Children.Count == 0
                            ? Empty()
                            : JsonElementExpr.ForEachProperty(
                                jsonElementExpr,
                                (jsonPropertyExpr, @continue) => MapPropertyExpression(jsonPropertyExpr, @continue)
                            )
                    )
                )
            );
            source.AddRange(GetValidations());
            source.Add(CreateInstanceExpr());

            return Block(
                GetVariables(),
                source
            );

            Expression MapPropertyExpression(Expression jsonPropertyExpr, LabelTarget @continue, int index = 0)
            {
                var child = Children[index];
                return IfThenElse(
                    child.TestExpr(jsonPropertyExpr),
                    child.MapExpr(jsonPropertyExpr),
                    index + 1 < Children.Count
                        ? MapPropertyExpression(jsonPropertyExpr, @continue, index + 1)
                        : UnexpectedPropertyBehavior == UnexpectedPropertyBehavior.Error
                            ? Expr.ThrowJsonException(Expr.UnexpectedProperty(Constant(Mapping.JsonPath, typeof(string)), jsonPropertyExpr))
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
                yield return ActualValueExpr;
                foreach (var child in Children)
                {
                    foreach (var variable in child.GetVariables())
                    {
                        yield return variable;
                    }
                }
            }
        }

        public override IEnumerable<Expression> GetValidations()
        {
            switch (Mapping.Significance)
            {
                case MappingSignificance.Required:
                {
                    yield return IfThen(
                        IsFalse(ObjectSeenExpr),
                        Throw(JsonExceptionExpr.Create(Constant($"Missing required value for '{Mapping.JsonPath}'.")))
                    );
                    foreach (var validation in Children.SelectMany(child => child.GetValidations()))
                    {
                        yield return validation;
                    }

                    break;
                }
                case MappingSignificance.Optional:
                    var childValidators = Children.SelectMany(child => child.GetValidations()).ToList();
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
