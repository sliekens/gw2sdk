using GW2SDK.Impl.JsonReaders.Mappings;
using GW2SDK.Impl.JsonReaders.Nodes;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders
{
    public partial class JsonMappingCompiler<TObject>
    {
        public void VisitProperty(JsonPropertyMapping mapping)
        {
            var propertySeenExpr = Variable(typeof(bool), $"{mapping.Name}_key_seen");
            if (mapping.Significance == MappingSignificance.Ignored)
            {
                Nodes.Push(
                    new PropertyNode
                    {
                        Mapping = mapping,
                        PropertySeenExpr = propertySeenExpr
                    }
                );
            }
            else
            {
                mapping.ValueNode.Accept(this);

                var value = Nodes.Pop();

                Nodes.Push(
                    new PropertyNode
                    {
                        Mapping = mapping,
                        ValueNode = value,
                        PropertySeenExpr = propertySeenExpr
                    }
                );
            }
        }
    }
}
