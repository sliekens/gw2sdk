using GW2SDK.Impl.JsonReaders.Linq;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Impl.JsonReaders
{
    public partial class JsonMappingCompiler<TRootElement>
    {
        public void VisitProperty(IJsonPropertyMapping mapping)
        {
            if (mapping.Significance == MappingSignificance.Ignored)
            {
                Nodes.Push(new PropertyDescriptor(mapping, null));
            }
            else
            {
                mapping.ValueMapping.Accept(this);

                var value = Nodes.Pop();

                Nodes.Push(new PropertyDescriptor(mapping, value));
            }
        }
    }
}
