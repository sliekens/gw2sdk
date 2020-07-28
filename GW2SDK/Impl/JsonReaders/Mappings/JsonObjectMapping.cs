using System.Collections.Generic;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TValue> : JsonMapping
    {
        public override JsonMappingKind Kind => JsonMappingKind.Object;

        public UnexpectedPropertyBehavior UnexpectedPropertyBehavior { get; set; } = UnexpectedPropertyBehavior.Error;

        public override void Accept(IJsonMappingVisitor visitor) => visitor.VisitObject(this);

        public List<JsonPropertyMapping> Children { get; } = new List<JsonPropertyMapping>();

        public void Ignore(string propertyName)
        {
            var jsonPropertyMapping = new JsonPropertyMapping
            {
                Name = propertyName,
                Significance = MappingSignificance.Ignored,
                ParentNode = this
            };

            Children.Add(jsonPropertyMapping);
        }


        public override string JsonPath => ParentNode?.JsonPath ?? "$";
    }
}
