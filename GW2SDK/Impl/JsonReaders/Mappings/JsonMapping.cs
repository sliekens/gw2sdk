namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public abstract class JsonMapping : IJsonMapping
    {
        public string Name { get; set; } = "";

        public MappingSignificance Significance { get; set; } = MappingSignificance.Required;

        public abstract void Accept(IJsonMappingVisitor visitor);
    }
}