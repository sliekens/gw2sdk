namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public interface IJsonMappingVisitor
    {
        void VisitObject<TObject>(JsonObjectMapping<TObject> mapping);

        void VisitValue<TValue>(JsonValueMapping<TValue> mapping);

        void VisitDeconstruction<TObject>(JsonDeconstructionMapping<TObject> mapping);

        void VisitProperty(JsonPropertyMapping mapping);

        void VisitArray<TValue>(JsonArrayMapping<TValue> mapping);
    }
}