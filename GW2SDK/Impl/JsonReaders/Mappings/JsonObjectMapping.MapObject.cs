using System;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TValue>
    {
        public void Map(string propertyName, Action<JsonObjectMapping<TValue>> map, MappingSignificance significance = MappingSignificance.Required)
        {
            var jsonObjectMapping = new JsonObjectMapping<TValue>
            {
                Significance = significance
            };
            var jsonPropertyMapping = new JsonPropertyMapping
            {
                Name = propertyName,
                Significance = significance,
                ValueNode = jsonObjectMapping,
                ParentNode = this
            };
            jsonObjectMapping.ParentNode = jsonPropertyMapping;
            map(jsonObjectMapping);
            Children.Add(jsonPropertyMapping);
        }
    }
}
