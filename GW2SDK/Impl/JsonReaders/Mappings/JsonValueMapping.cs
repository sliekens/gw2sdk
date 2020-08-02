namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public class JsonValueMapping<TValue> : JsonMapping
    {
        public override JsonMappingKind Kind => JsonMappingKind.Value;

        public JsonValueMappingKind ValueKind { get; set; }

        public IJsonReader<TValue>? JsonReader { get; set; }

        public override void Accept(IJsonMappingVisitor visitor)
        {
            visitor.VisitValue(this);
        }
    }
}
