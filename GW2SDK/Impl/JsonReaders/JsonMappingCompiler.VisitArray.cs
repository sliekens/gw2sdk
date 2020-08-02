using GW2SDK.Impl.JsonReaders.Mappings;
using GW2SDK.Impl.JsonReaders.Nodes;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders
{
    public partial class JsonMappingCompiler<TObject>
    {
        public void VisitArray<TValue>(JsonArrayMapping<TValue> mapping)
        {
            mapping.ValueMapping.Accept(this);

            var itemNode = Nodes.Pop();

            Nodes.Push(
                new ArrayNode
                {
                    Mapping = mapping,
                    ItemNode = itemNode,
                    ItemType = typeof(TValue),
                    ArraySeenExpr = Variable(typeof(bool),       $"{mapping.Name}_array_seen"),
                    ActualValueExpr = Variable(typeof(TValue[]), $"{mapping.Name}_value")
                }
            );
        }
    }
}
