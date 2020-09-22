using System.Collections.Generic;
using GW2SDK.Impl.JsonReaders.Linq;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Impl.JsonReaders
{
    public partial class JsonMappingCompiler<TRootElement>
    {
        public void VisitObject(IJsonObjectMapping mapping)
        {
            var properties = new List<PropertyDescriptor>();
            foreach (var child in mapping.Children)
            {
                child.Accept(this);
                var propertyNode = (PropertyDescriptor) Nodes.Pop();
                properties.Add(propertyNode);
            }

            Nodes.Push
            (
                new ObjectDescriptor(mapping)
                {
                    Properties = properties
                }
            );
        }
    }
}
