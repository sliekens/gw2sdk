using System.Reflection;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public class JsonPropertyMapping : JsonMapping, IJsonPropertyMapping
    {
        public IJsonMapping ValueMapping { get; set; } = default!;

        public override void Accept(IJsonMappingVisitor visitor)
        {
            visitor.VisitProperty(this);
        }

        public MemberInfo? Destination { get; set; }
    }
}
