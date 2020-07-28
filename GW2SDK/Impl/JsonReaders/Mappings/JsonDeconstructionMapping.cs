namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public class JsonDeconstructionMapping<TValue> : JsonObjectMapping<TValue>
    {
        public override JsonMappingKind Kind => JsonMappingKind.Deconstruction;

        public override void Accept(IJsonMappingVisitor visitor)
        {
            visitor.VisitDeconstruction(this);
        }
    }
}