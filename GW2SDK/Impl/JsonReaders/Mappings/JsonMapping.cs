using System.IO;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public abstract class JsonMapping : IJsonMapping
    {
        public string Name { get; set; } = GetRandomName();

        public MappingSignificance Significance { get; set; } = MappingSignificance.Required;

        public abstract void Accept(IJsonMappingVisitor visitor);

        private static string GetRandomName()
        {
            // TODO: is there a better way to get a random, short name?
            return "tmp" + Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
        }
    }
}