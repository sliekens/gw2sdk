using GW2SDK.Impl.JsonReaders.Mappings;
using GW2SDK.Impl.JsonReaders.Nodes;

namespace GW2SDK.Impl.JsonReaders
{
    public partial class JsonMappingCompiler<TRootElement>
    {
        public void VisitArray(IJsonArrayMapping mapping)
        {
            mapping.ValueMapping.Accept(this);

            var itemNode = Nodes.Pop();

            Nodes.Push(new ArrayNode(mapping, itemNode));
        }
    }
}
