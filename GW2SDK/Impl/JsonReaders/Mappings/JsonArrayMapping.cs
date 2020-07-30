namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public class JsonArrayMapping<TValue> : JsonMapping
    {
        public override JsonMappingKind Kind => JsonMappingKind.Array;

        public JsonValueMapping<TValue> ValueMapping { get; set; } = default!;

        public override void Accept(IJsonMappingVisitor visitor) => visitor.VisitArray(this);

        public override string JsonPath => ParentNode?.JsonPath ?? "$";
    }
}
