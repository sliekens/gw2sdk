namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public class JsonPropertyMapping : JsonMapping
    {
        public override JsonMappingKind Kind => JsonMappingKind.Property;

        public JsonMapping ValueNode { get; set; } = default!;

        public override void Accept(IJsonMappingVisitor visitor)
        {
            visitor.VisitProperty(this);
        }

        public override string JsonPath => $"{ParentNode!.JsonPath}.{Name}";
    }
}
