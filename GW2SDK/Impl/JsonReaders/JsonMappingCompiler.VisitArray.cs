using GW2SDK.Impl.JsonReaders.Mappings;
using GW2SDK.Impl.JsonReaders.Nodes;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders
{
    public partial class JsonMappingCompiler<TObject>
    {
        public void VisitArray<TValue>(JsonArrayMapping<TValue> mapping)
        {
            var name = mapping.ParentNode?.Name ?? mapping.Name;
            Nodes.Push(
                new ArrayNode
                {
                    Mapping = mapping,
                    ArraySeenExpr = Variable(typeof(bool),       $"{name}_array_seen"),
                    ActualValueExpr = Variable(typeof(TValue[]), $"{name}_value")
                }
            );
        }
    }
}
