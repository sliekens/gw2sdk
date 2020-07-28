using System.Linq.Expressions;
using GW2SDK.Impl.JsonReaders.Mappings;
using GW2SDK.Impl.JsonReaders.Nodes;

namespace GW2SDK.Impl.JsonReaders
{
    public partial class JsonReaderCompiler<TObject>
    {
        public void VisitProperty(JsonPropertyMapping mapping)
        {
            mapping.ValueNode.Accept(this);

            var value = Nodes.Pop();

            Nodes.Push(new PropertyNode
            {
                Mapping = mapping,
                ValueNode = value,
                ValueSeenExpr = Expression.Variable(typeof(bool), $"{mapping.Name}_seen")
            });
        }
    }
}
