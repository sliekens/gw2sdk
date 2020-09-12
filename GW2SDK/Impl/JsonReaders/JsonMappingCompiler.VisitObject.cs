using System.Collections.Generic;
using GW2SDK.Impl.JsonReaders.Mappings;
using GW2SDK.Impl.JsonReaders.Nodes;

namespace GW2SDK.Impl.JsonReaders
{
    public partial class JsonMappingCompiler<TRootElement>
    {
        public void VisitObject(IJsonObjectMapping mapping)
        {
            var properties = new List<PropertyNode>();
            foreach (var child in mapping.Children)
            {
                child.Accept(this);
                var propertyNode = (PropertyNode) Nodes.Pop();
                properties.Add(propertyNode);
            }

            Nodes.Push
            (
                new ObjectNode(mapping)
                {
                    Properties = properties
                }
            );
        }
    }
}
