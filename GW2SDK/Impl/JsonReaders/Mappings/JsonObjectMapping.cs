using System.Collections.Generic;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject> : JsonMapping
    {
        public UnexpectedPropertyBehavior UnexpectedPropertyBehavior { get; set; } = UnexpectedPropertyBehavior.Error;

        public List<JsonPropertyMapping> Children { get; } = new List<JsonPropertyMapping>();

        public override void Accept(IJsonMappingVisitor visitor) => visitor.VisitObject(this);

        public void Ignore(string propertyName)
        {
            Children.Add(new JsonPropertyMapping
            {
                Name = propertyName,
                Significance = MappingSignificance.Ignored
            });
        }
    }
}
