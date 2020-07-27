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

        public RootState Root { get; set; } = new RootState();

        public Expression<ReadJson<TObject>>? Source { get; set; }

        public void VisitAggregate<TValue>(JsonAggregateMapping<TValue> mapping)
        {
            foreach (var child in mapping.Children)
            {
                child.Accept(this);
            }

            var source = new List<Expression>();
            source.Add(
                JsonElementExpr.ForEachProperty(
                    Root.InputExpr,
                    Root.JsonPropertyExpr,
                    @continue => MapPropertyExpression(@continue)
                )
            );

            source.AddRange(
                from child in Root.Children
                where child.Mapping.Significance == MappingSignificance.Required
                select Expr.EnsureMemberseen(child.Mapping.Name, child.ChildSeenExpr, typeof(TObject).Name)
            );

            source.Add(
                MemberInit(
                    New(typeof(TObject)),
                    from child in Root.Children
                    where child.Mapping.Significance != MappingSignificance.Ignored
                    select Bind(child.Mapping.Destination, child.ChildValueExpr)
                )
            );

            var body = Block(
                Root.GetVariables(),
                source
            );

            Source = Lambda<ReadJson<TObject>>(body, Root.InputExpr);

            Expression MapPropertyExpression(LabelTarget @continue, int index = 0)
            {
                var child = Root.Children[index];
                return IfThenElse(
                    child.TestExpr,
                    child.MapExpr,
                    index + 1 < Root.Children.Count
                        ? MapPropertyExpression(@continue, index + 1)
                        : UnexpectedPropertyBehavior == UnexpectedPropertyBehavior.Error
                            ? Expr.ThrowJsonException(Expr.MissingMember(Constant(typeof(TObject).Name), Root.JsonPropertyExpr))
                            : Continue(@continue)
                );
            }
        }

        public void VisitObject<TValue>(JsonObjectMapping<TValue> mapping)
        {
        }

        public void VisitValue<TValue>(JsonValueMapping<TValue> mapping)
        {
            var propertySeenExpr = Variable(typeof(bool), $"{mapping.Name}_seen");
            var propertyValueExpr = Variable(typeof(int), $"{mapping.Name}_value");
            ChildState state = new ChildState
            {
                Mapping = mapping,
                ChildSeenExpr = propertySeenExpr,
                ChildValueExpr = propertyValueExpr,
                TestExpr = JsonPropertyExpr.NameEquals(Root.JsonPropertyExpr, Constant(mapping.Name, typeof(string))),
                MapExpr = Block(
                    Assign(propertySeenExpr,  Constant(true)),
                    Assign(propertyValueExpr, JsonElementExpr.GetInt32(JsonPropertyExpr.GetValue(Root.JsonPropertyExpr)))
                )
            };

            Root.Children.Add(state);
        }
    }

    public class RootState
    {
        public ParameterExpression InputExpr { get; } = Parameter(typeof(JsonElement).MakeByRefType(), "json");

        public ParameterExpression JsonPropertyExpr { get; } = Variable(typeof(JsonProperty), "json_property");

        public List<ChildState> Children { get; set; } = new List<ChildState>();

        public IEnumerable<ParameterExpression> GetVariables()
        {
            foreach (var child in Children)
            {
                foreach (var variable in child.GetVariables())
                {
                    yield return variable;
                }
            }
        }
    }

    public class ChildState
    {
        public JsonMapping Mapping { get; set; } = default!;

        public ParameterExpression ChildSeenExpr { get; set; } = default!;

        public ParameterExpression ChildValueExpr { get; set; } = default!;

        public Expression TestExpr { get; set; } = default!;

        public Expression MapExpr { get; set; } = default!;

        public IEnumerable<ParameterExpression> GetVariables()
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
    }
}
