using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using GW2SDK.Impl.Json;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Impl.JsonReaders.Nodes
{
    public class ObjectNode : JsonNode
    {
        public List<PropertyNode> Children { get; set; } = new List<PropertyNode>();

        public UnexpectedPropertyBehavior UnexpectedPropertyBehavior { get; set; }

        public Expression MapExpr(Expression jsonElementExpr)
        {
            if (Mapping.Significance == MappingSignificance.Ignored)
            {
                return Expression.Empty();
            }

            return Expression.IfThen(
                Expression.Equal(JsonElementExpr.GetValueKind(jsonElementExpr), Expression.Constant(JsonValueKind.Object)),
                Expression.Block(
                    Expression.Assign(ValueSeenExpr, Expression.Constant(true)),
                    Children.Count == 0
                        ? Expression.Empty()
                        : JsonElementExpr.ForEachProperty(jsonElementExpr, (jsonPropertyExpr, @continue) => MapPropertyExpression(jsonPropertyExpr, @continue))
                )
            );

            Expression MapPropertyExpression(Expression jsonPropertyExpr, LabelTarget @continue, int index = 0)
            {
                var child = Children[index];
                return Expression.IfThenElse(
                    child.TestExpr(jsonPropertyExpr),
                    child.MapExpr(jsonPropertyExpr),
                    index + 1 < Children.Count
                        ? MapPropertyExpression(jsonPropertyExpr, @continue, index + 1)
                        : UnexpectedPropertyBehavior == UnexpectedPropertyBehavior.Error
                            ? Expr.ThrowJsonException(Expr.UnexpectedProperty(jsonPropertyExpr))
                            : Expression.Continue(@continue)
                );
            }
        }

        public override IEnumerable<ParameterExpression> GetVariables()
        {
            if (Mapping.Significance != MappingSignificance.Ignored)
            {
                yield return ValueSeenExpr;
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
                    yield return Expression.IfThen(
                        Expression.IsFalse(ValueSeenExpr),
                        Expression.Throw(JsonExceptionExpr.Create(Expression.Constant($"Missing required value for '{Mapping.JsonPath}'.")))
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
                        yield return Expression.IfThen(
                            Expression.IsTrue(ValueSeenExpr),
                            Expression.Block(childValidators)
                        );
                    }
                    break;
            }
        }

        public override IEnumerable<MemberBinding> GetBindings() => Children.SelectMany(child => child.GetBindings());
    }
}