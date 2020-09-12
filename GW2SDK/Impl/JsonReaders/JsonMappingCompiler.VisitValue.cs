using GW2SDK.Impl.JsonReaders.Mappings;
using GW2SDK.Impl.JsonReaders.Nodes;

namespace GW2SDK.Impl.JsonReaders
{
    public partial class JsonMappingCompiler<TRootElement>
    {
        public void VisitValue(IJsonValueMapping mapping)
        {
            Nodes.Push(new ValueNode(mapping));
        }
    }
}
