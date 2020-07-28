using System.Collections.Generic;
using GW2SDK.Impl.JsonReaders.Mappings;
using GW2SDK.Impl.JsonReaders.Nodes;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders
{
    public partial class JsonReaderCompiler<TObject>
    {
        public void VisitAggregate<TValue>(JsonAggregateMapping<TValue> mapping)
        {
            var name = mapping.ParentNode?.Name ?? mapping.Name;
            var nodes = new List<PropertyNode>();
            foreach (var child in mapping.Children)
            {
                child.Accept(this);
                var propertyNode = (PropertyNode) Nodes.Pop();
                nodes.Add(propertyNode);
            }

            Nodes.Push(
                new RootNode
                {
                    Mapping = mapping,
                    Children = nodes,
                    UnexpectedPropertyBehavior = mapping.UnexpectedPropertyBehavior,
                    ValueSeenExpr = Variable(typeof(bool), $"{name}_seen")
                }
            );
        }
    }
}
