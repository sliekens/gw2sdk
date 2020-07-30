using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map<TProperty>(
            string propertyName,
            Expression<Func<TObject, TProperty>> custom,
            IJsonReader<TProperty> jsonReader,
            MappingSignificance significance = MappingSignificance.Required)
        {
            var jsonValueMapping = new JsonValueMapping<TProperty>
            {
                ValueKind = JsonValueMappingKind.Custom,
                JsonReader = jsonReader,
                Significance = significance
            };

            var jsonPropertyMapping = new JsonPropertyMapping
            {
                Name = propertyName,
                Destination = ((MemberExpression) custom.Body).Member,
                Significance = significance,
                ValueNode = jsonValueMapping,
                ParentNode = this
            };

            jsonValueMapping.ParentNode = jsonPropertyMapping;
            Children.Add(jsonPropertyMapping);
        }
        
        public void Map<TProperty>(
            string propertyName,
            Expression<Func<TObject, IEnumerable<TProperty>?>> custom,
            IJsonReader<TProperty> jsonReader,
            MappingSignificance significance = MappingSignificance.Required,
            MappingSignificance itemSignificance = MappingSignificance.Required)
        {
            var jsonValueMapping = new JsonValueMapping<TProperty>
            {
                ValueKind = JsonValueMappingKind.Custom,
                JsonReader = jsonReader,
                Significance = itemSignificance
            };

            var jsonArrayMapping = new JsonArrayMapping<TProperty>()
            {
                ValueMapping = jsonValueMapping,
                Significance = significance
            };

            jsonValueMapping.ParentNode = jsonArrayMapping;

            var jsonPropertyMapping = new JsonPropertyMapping
            {
                Name = propertyName,
                Destination = ((MemberExpression) custom.Body).Member,
                Significance = significance,
                ValueNode = jsonArrayMapping,
                ParentNode = this
            };

            jsonArrayMapping.ParentNode = jsonPropertyMapping;
            Children.Add(jsonPropertyMapping);
        }
    }
}
