using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using GW2SDK.Impl.Json;
using GW2SDK.Impl.JsonReaders.Mappings;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders
{
    public class JsonReaderCompiler<TObject> : IJsonMappingVisitor
    {
        public UnexpectedPropertyBehavior UnexpectedPropertyBehavior { get; set; }

        public Stack<JsonReaderState> Nodes { get; } = new Stack<JsonReaderState>();

        public Expression<ReadJson<TObject>>? Source { get; set; }

        public void VisitAggregate<TValue>(JsonAggregateMapping<TValue> mapping)
        {
            var inputExpr = Parameter(typeof(JsonElement).MakeByRefType(), "json");
            var rootNode = new RootNodeState
            {
                JsonElementExpr = inputExpr,
                JsonPropertyExpr = Variable(typeof(JsonProperty), "root_property")
            };
            Nodes.Push(rootNode);
            foreach (var child in mapping.Children)
            {
                child.Accept(this);
            }

            Nodes.Pop();

            var source = new List<Expression>();
            if (mapping.Children.Count != 0)
            {
                source.Add(
                    JsonElementExpr.ForEachProperty(
                        rootNode.JsonElementExpr,
                        rootNode.JsonPropertyExpr,
                        @continue => MapPropertyExpression(@continue)
                    )
                );
            }

            source.AddRange(
                from child in rootNode.Children
                where child.Mapping.Significance == MappingSignificance.Required
                select child.MissingExpr
            );

            source.Add(
                MemberInit(
                    New(typeof(TObject)),
                    rootNode.GetBindings()                )
            );

            var body = Block(
                rootNode.GetVariables(),
                source
            );

            Source = Lambda<ReadJson<TObject>>(body, inputExpr);

            Expression MapPropertyExpression(LabelTarget @continue, int index = 0)
            {
                var child = rootNode.Children[index];
                return IfThenElse(
                    child.TestExpr,
                    child.MapExpr,
                    index + 1 < rootNode.Children.Count
                        ? MapPropertyExpression(@continue, index + 1)
                        : UnexpectedPropertyBehavior == UnexpectedPropertyBehavior.Error
                            ? Expr.ThrowJsonException(Expr.MissingMember(Constant(typeof(TObject).Name), rootNode.JsonPropertyExpr))
                            : Continue(@continue)
                );
            }
        }

        public void VisitObject<TValue>(JsonObjectMapping<TValue> mapping)
        {
            var parentNode = (ObjectNodeState)Nodes.Peek();

            var currentNode = new ObjectNodeState
            {
                Mapping = mapping,
                JsonElementExpr = JsonPropertyExpr.GetValue(parentNode.JsonPropertyExpr),
                JsonPropertyExpr = Variable(typeof(JsonProperty), $"{mapping.Name}_property"),
                TestExpr = JsonPropertyExpr.NameEquals(parentNode.JsonPropertyExpr, Constant(mapping.Name, typeof(string)))
            };

            Nodes.Push(currentNode);
            foreach (var child in mapping.Children)
            {
                child.Accept(this);
            }

            Nodes.Pop();

            var source = new List<Expression>();
            if (mapping.Children.Count != 0)
            {
                source.Add(
                    JsonElementExpr.ForEachProperty(
                        currentNode.JsonElementExpr,
                        currentNode.JsonPropertyExpr,
                        @continue => MapPropertyExpression(@continue)
                    )
                );
            }

            source.AddRange(
                from child in currentNode.Children
                where child.Mapping.Significance == MappingSignificance.Required
                select child.MissingExpr
            );

            currentNode.MapExpr = Block(source);
            parentNode.Children.Add(currentNode);

            Expression MapPropertyExpression(LabelTarget @continue, int index = 0)
            {
                var child = currentNode.Children[index];
                return IfThenElse(
                    child.TestExpr,
                    child.MapExpr,
                    index + 1 < currentNode.Children.Count
                        ? MapPropertyExpression(@continue, index + 1)
                        : UnexpectedPropertyBehavior == UnexpectedPropertyBehavior.Error
                            ? Expr.ThrowJsonException(Expr.MissingMember(Constant(typeof(TObject).Name), currentNode.JsonPropertyExpr))
                            : Continue(@continue)
                );
            }
        }

        public void VisitValue<TValue>(JsonValueMapping<TValue> mapping)
        {
            var propertySeenExpr = Variable(typeof(bool), $"{mapping.Name}_seen");
            var propertyValueExpr = Variable(typeof(int), $"{mapping.Name}_value");
            var node = (ObjectNodeState)Nodes.Peek();
            node.Children.Add(new ValueChildState
            {
                Mapping = mapping,
                ChildSeenExpr = propertySeenExpr,
                ChildValueExpr = propertyValueExpr,
                TestExpr = JsonPropertyExpr.NameEquals(node.JsonPropertyExpr, Constant(mapping.Name, typeof(string))),
                MapExpr = Block(
                    Assign(propertySeenExpr,  Constant(true)),
                    Assign(propertyValueExpr, JsonElementExpr.GetInt32(JsonPropertyExpr.GetValue(node.JsonPropertyExpr)))
                ),
                MissingExpr = Expr.EnsureMemberseen(mapping.Name, propertySeenExpr, typeof(TObject).Name),
                BindExpr = propertyValueExpr
            });
        }
    }

    public abstract class JsonReaderState
    {
        public List<JsonReaderState> Children { get; set; } = new List<JsonReaderState>();

        public JsonMapping Mapping { get; set; } = default!;

        public Expression TestExpr { get; set; } = default!;

        public Expression MapExpr { get; set; } = default!;

        public Expression MissingExpr { get; set; } = default!;

        public Expression BindExpr { get; set; } = default!;

        public abstract IEnumerable<ParameterExpression> GetVariables();

        public abstract IEnumerable<MemberBinding> GetBindings();
    }

    public class RootNodeState : ObjectNodeState
    {
    }

    public class ObjectNodeState : JsonReaderState
    {
        public Expression JsonElementExpr { get; set; } = default!;

        public ParameterExpression JsonPropertyExpr { get; set; } = default!;
        
        public override IEnumerable<ParameterExpression> GetVariables()
        {
            foreach (var child in Children)
            {
                foreach (var variable in child.GetVariables())
                {
                    yield return variable;
                }
            }
        }

        public override IEnumerable<MemberBinding> GetBindings()
        {
            foreach (var child in Children)
            {
                foreach (var binding in child.GetBindings())
                {
                    yield return binding;
                }
            }
        }
    }

    public class ValueChildState : JsonReaderState
    {
        public ParameterExpression ChildSeenExpr { get; set; } = default!;

        public ParameterExpression ChildValueExpr { get; set; } = default!;

        public override IEnumerable<ParameterExpression> GetVariables()
        {
            if (Mapping.Significance == MappingSignificance.Required)
            {
                yield return ChildSeenExpr;
            }

            if (Mapping.Significance != MappingSignificance.Ignored)
            {
                yield return ChildValueExpr;
            }
        }

        public override IEnumerable<MemberBinding> GetBindings()
        {
            if (Mapping.Significance == MappingSignificance.Ignored)
            {
                yield break;
            }
            yield return Bind(Mapping.Destination, ChildValueExpr);
        }
    }
}
