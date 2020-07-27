namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public interface IJsonMappingVisitor
    {
        void VisitAggregate<TValue>(JsonAggregateMapping<TValue> mapping);

        void VisitValue<TValue>(JsonValueMapping<TValue> mapping);

        void VisitObject<TValue>(JsonObjectMapping<TValue> mapping);
    }
}