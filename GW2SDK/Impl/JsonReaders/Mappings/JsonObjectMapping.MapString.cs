using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map(
            string propertyName,
            Expression<Func<TObject, string>> propertyExpression,
            MappingSignificance significance = MappingSignificance.Required)
        {
            var jsonValueMapping = new JsonValueMapping<string>
            {
                ValueKind = JsonValueMappingKind.String,
                Significance = significance
            };

            var jsonPropertyMapping = new JsonPropertyMapping
            {
                Name = propertyName,
                Destination = ((MemberExpression) propertyExpression.Body).Member,
                Significance = significance,
                ValueNode = jsonValueMapping,
                ParentNode = this
            };

            jsonValueMapping.ParentNode = jsonPropertyMapping;
            Children.Add(jsonPropertyMapping);
        }
        
        public void Map(
            string propertyName,
            Expression<Func<TObject, IEnumerable<string>?>> propertyExpression,
            MappingSignificance significance = MappingSignificance.Required,
            MappingSignificance itemSignificance = MappingSignificance.Required)
        {
            var jsonValueMapping = new JsonValueMapping<string>
            {
                Name = "kakitem",
                ValueKind = JsonValueMappingKind.String,
                Significance = itemSignificance
            };

            var jsonArrayMapping = new JsonArrayMapping<string>()
            {
                ValueMapping = jsonValueMapping,
                Significance = significance
            };

            jsonValueMapping.ParentNode = jsonArrayMapping;

            var jsonPropertyMapping = new JsonPropertyMapping
            {
                Name = propertyName,
                Destination = ((MemberExpression) propertyExpression.Body).Member,
                Significance = significance,
                ValueNode = jsonArrayMapping,
                ParentNode = this
            };

            jsonArrayMapping.ParentNode = jsonPropertyMapping;
            Children.Add(jsonPropertyMapping);
        }
    }
}
