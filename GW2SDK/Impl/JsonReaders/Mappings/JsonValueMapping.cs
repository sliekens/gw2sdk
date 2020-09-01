namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public class JsonValueMapping<TValue> : JsonMapping
    {
        public JsonValueMappingKind ValueKind { get; set; }

        public ConvertJsonElement<TValue>? JsonConverter { get; set; }

        public override void Accept(IJsonMappingVisitor visitor)
        {
            visitor.VisitValue(this);
        }
    }
}
