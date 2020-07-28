using System.Collections.Generic;
using System.Text.Json;
using GW2SDK.Impl.Json;
using GW2SDK.Impl.JsonReaders.Mappings;
using GW2SDK.Impl.JsonReaders.Nodes;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders
{
    public partial class JsonReaderCompiler<TObject>
    {
        public void VisitObject<TValue>(JsonObjectMapping<TValue> mapping)
        {
            var name = mapping.ParentNode?.Name ?? mapping.Name;
            var nodes = new List<PropertyNode>();
            foreach (var child in mapping.Children)
            {
                child.Accept(this);
                var propertyNode = (PropertyNode)Nodes.Pop();
                nodes.Add(propertyNode);
            }

            Nodes.Push(new ObjectNode
            {
                Mapping = mapping,
                Children = nodes,
                UnexpectedPropertyBehavior = mapping.UnexpectedPropertyBehavior,
                ValueSeenExpr = Variable(typeof(bool), $"{name}_value_seen")
            });

            //var objectSeenExpr = Expression.Variable(typeof(bool), $"{mapping.Name}_seen");
            //var parentNode = (ObjectNodeState) Nodes.Peek();

            //var currentNode = new ObjectNodeState
            //{
            //    Mapping = mapping,
            //    ValueSeenExpr = objectSeenExpr,
            //    JsonElementExpr = JsonPropertyExpr.GetValue(parentNode.JsonPropertyExpr),
            //    JsonPropertyExpr = Expression.Variable(typeof(JsonProperty), $"{mapping.Name}_property"),
            //    TestExpr = JsonPropertyExpr.NameEquals(parentNode.JsonPropertyExpr, Expression.Constant(mapping.Name, typeof(string)))
            //};

            //Nodes.Push(currentNode);
            //foreach (var child in mapping.Children)
            //{
            //    child.Accept(this);
            //}

            //Nodes.Pop();

            //var source = new List<Expression>();
            //source.Add(Expression.Assign(objectSeenExpr, Expression.Constant(true)));
            //if (mapping.Children.Count != 0)
            //{
            //    source.Add(
            //        JsonElementExpr.ForEachProperty(
            //            currentNode.JsonElementExpr,
            //            currentNode.JsonPropertyExpr,
            //            @continue => MapPropertyExpression(@continue)
            //        )
            //    );
            //}

            //currentNode.MapExpr = Expression.Block(source);
            //parentNode.Children.Add(currentNode);

            //Expression MapPropertyExpression(LabelTarget @continue, int index = 0)
            //{
            //    var child = currentNode.Children[index];
            //    return Expression.IfThenElse(
            //        child.TestExpr,
            //        child.MapExpr,
            //        index + 1 < currentNode.Children.Count
            //            ? MapPropertyExpression(@continue, index + 1)
            //            : UnexpectedPropertyBehavior == UnexpectedPropertyBehavior.Error
            //                ? Expr.ThrowJsonException(Expr.MissingMember(Expression.Constant(typeof(TObject).Name), currentNode.JsonPropertyExpr))
            //                : Expression.Continue(@continue)
            //    );
            //}
        }
    }
}
