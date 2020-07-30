namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public class JsonDeconstructionMapping<TObject> : JsonObjectMapping<TObject>
    {
        public override JsonMappingKind Kind => JsonMappingKind.Deconstruction;

        public override void Accept(IJsonMappingVisitor visitor)
        {
            visitor.VisitDeconstruction(this);
        }
    }
}