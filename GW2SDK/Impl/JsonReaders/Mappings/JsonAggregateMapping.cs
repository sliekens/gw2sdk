namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public class JsonAggregateMapping<TValue> : JsonObjectMapping<TValue>
    {
        public override JsonMappingKind Kind => JsonMappingKind.Aggregate;

        public override void Accept(IJsonMappingVisitor visitor) => visitor.VisitAggregate(this);
    }
}
