namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public interface IJsonMappingVisitor
    {
        void VisitObject<TValue>(JsonObjectMapping<TValue> mapping);

        void VisitValue<TValue>(JsonValueMapping<TValue> mapping);

        void VisitDeconstruction<TValue>(JsonDeconstructionMapping<TValue> mapping);

        void VisitProperty(JsonPropertyMapping mapping);
    }
}