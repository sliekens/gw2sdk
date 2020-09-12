using GW2SDK.Impl.JsonReaders.Mappings;
using GW2SDK.Impl.JsonReaders.Nodes;

namespace GW2SDK.Impl.JsonReaders
{
    public partial class JsonMappingCompiler<TRootElement>
    {
        public void VisitProperty(IJsonPropertyMapping mapping)
        {
            if (mapping.Significance == MappingSignificance.Ignored)
            {
                Nodes.Push(new PropertyNode(mapping, null));
            }
            else
            {
                mapping.ValueMapping.Accept(this);

                var value = Nodes.Pop();

                Nodes.Push(new PropertyNode(mapping, value));
            }
        }
    }
}
