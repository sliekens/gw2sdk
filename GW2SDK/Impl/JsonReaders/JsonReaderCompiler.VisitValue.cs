using GW2SDK.Impl.JsonReaders.Mappings;
using GW2SDK.Impl.JsonReaders.Nodes;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders
{
    public partial class JsonReaderCompiler<TObject>
    {
        public void VisitValue<TValue>(JsonValueMapping<TValue> mapping)
        {
            var name = mapping.ParentNode?.Name ?? mapping.Name;
            Nodes.Push(
                new LeafNode
                {
                    Mapping = mapping,
                    ValueSeenExpr = Variable(typeof(bool),     $"{name}_value_seen"),
                    ActualValueExpr = Variable(typeof(TValue), $"{name}_value")
                }
            );
        }
    }
}
