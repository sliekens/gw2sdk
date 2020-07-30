using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map(string propertyName, Expression<Func<TObject, int>> int32)
        {
            var jsonValueMapping = new JsonValueMapping<int>
            {
                ValueKind = JsonValueMappingKind.Int32,
                Significance = MappingSignificance.Required
            };

            var jsonPropertyMapping = new JsonPropertyMapping
            {
                Name = propertyName,
                Destination = ((MemberExpression) int32.Body).Member,
                Significance = MappingSignificance.Required,
                ValueNode = jsonValueMapping,
                ParentNode = this
            };

            jsonValueMapping.ParentNode = jsonPropertyMapping;
            Children.Add(jsonPropertyMapping);
        }

        public void Map(string propertyName, Expression<Func<TObject, int?>> int32)
        {
            var jsonValueMapping = new JsonValueMapping<int?>
            {
                ValueKind = JsonValueMappingKind.Int32,
                Significance = MappingSignificance.Optional
            };

            var jsonPropertyMapping = new JsonPropertyMapping
            {
                Name = propertyName,
                Destination = ((MemberExpression) int32.Body).Member,
                Significance = MappingSignificance.Optional,
                ValueNode = jsonValueMapping,
                ParentNode = this
            };

            jsonValueMapping.ParentNode = jsonPropertyMapping;
            Children.Add(jsonPropertyMapping);
        }
        
        public void Map(
            string propertyName,
            Expression<Func<TObject, IEnumerable<int>?>> int32s,
            MappingSignificance significance = MappingSignificance.Required)
        {
            var jsonValueMapping = new JsonValueMapping<int>
            {
                ValueKind = JsonValueMappingKind.Int32,
                Significance = MappingSignificance.Required
            };

            var jsonArrayMapping = new JsonArrayMapping<int>
            {
                ValueMapping = jsonValueMapping,
                Significance = MappingSignificance.Required
            };

            jsonValueMapping.ParentNode = jsonArrayMapping;

            var jsonPropertyMapping = new JsonPropertyMapping
            {
                Name = propertyName,
                Destination = ((MemberExpression) int32s.Body).Member,
                Significance = significance,
                ValueNode = jsonArrayMapping,
                ParentNode = this
            };

            jsonArrayMapping.ParentNode = jsonPropertyMapping;
            Children.Add(jsonPropertyMapping);
        }
        
        public void Map(
            string propertyName,
            Expression<Func<TObject, IEnumerable<int?>?>> int32s,
            MappingSignificance significance = MappingSignificance.Required)
        {
            var jsonValueMapping = new JsonValueMapping<int?>
            {
                ValueKind = JsonValueMappingKind.Int32,
                Significance = MappingSignificance.Optional
            };

            var jsonArrayMapping = new JsonArrayMapping<int?>()
            {
                ValueMapping = jsonValueMapping,
                Significance = MappingSignificance.Optional
            };

            jsonValueMapping.ParentNode = jsonArrayMapping;

            var jsonPropertyMapping = new JsonPropertyMapping
            {
                Name = propertyName,
                Destination = ((MemberExpression) int32s.Body).Member,
                Significance = significance,
                ValueNode = jsonArrayMapping,
                ParentNode = this
            };

            jsonArrayMapping.ParentNode = jsonPropertyMapping;
            Children.Add(jsonPropertyMapping);
        }
    }
}
