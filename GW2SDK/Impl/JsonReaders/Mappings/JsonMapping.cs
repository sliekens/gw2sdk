using System.Reflection;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public abstract class JsonMapping
    {
        public string Name { get; set; } = "";

        public JsonMapping? ParentNode { get; set; }

        public abstract JsonMappingKind Kind { get; }

        public MappingSignificance Significance { get; set; } = MappingSignificance.Required;

        public MemberInfo? Destination { get; set; }

        public abstract void Accept(IJsonMappingVisitor visitor);

        public abstract string JsonPath { get; }
    }
}