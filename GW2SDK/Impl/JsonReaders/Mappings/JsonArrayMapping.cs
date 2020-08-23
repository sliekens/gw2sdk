namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public class JsonArrayMapping<TValue> : JsonMapping
    {
        public JsonMapping ValueMapping { get; set; } = default!;

        public override void Accept(IJsonMappingVisitor visitor) => visitor.VisitArray(this);
    }
}
