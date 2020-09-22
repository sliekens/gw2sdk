using GW2SDK.Impl.JsonReaders.Linq;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Impl.JsonReaders
{
    public partial class JsonMappingCompiler<TRootElement>
    {
        public void VisitArray(IJsonArrayMapping mapping)
        {
            mapping.ValueMapping.Accept(this);

            var itemNode = Nodes.Pop();

            Nodes.Push(new ArrayDescriptor(mapping, itemNode));
        }
    }
}
