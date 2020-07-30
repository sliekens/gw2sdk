using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map(string propertyName, Expression<Func<TObject, Guid>> propertyExpression)
        {
            var jsonValueMapping = new JsonValueMapping<Guid>
            {
                ValueKind = JsonValueMappingKind.Guid,
                Significance = MappingSignificance.Required
            };

            var jsonPropertyMapping = new JsonPropertyMapping
            {
                Name = propertyName,
                Destination = ((MemberExpression) propertyExpression.Body).Member,
                Significance = MappingSignificance.Required,
                ValueNode = jsonValueMapping,
                ParentNode = this
            };

            jsonValueMapping.ParentNode = jsonPropertyMapping;
            Children.Add(jsonPropertyMapping);
        }

        public void Map(string propertyName, Expression<Func<TObject, Guid?>> propertyExpression)
        {
            var jsonValueMapping = new JsonValueMapping<Guid?>
            {
                ValueKind = JsonValueMappingKind.Guid,
                Significance = MappingSignificance.Optional
            };

            var jsonPropertyMapping = new JsonPropertyMapping
            {
                Name = propertyName,
                Destination = ((MemberExpression) propertyExpression.Body).Member,
                Significance = MappingSignificance.Optional,
                ValueNode = jsonValueMapping,
                ParentNode = this
            };

            jsonValueMapping.ParentNode = jsonPropertyMapping;
            Children.Add(jsonPropertyMapping);
        }
        
        public void Map(
            string propertyName,
            Expression<Func<TObject, IEnumerable<Guid>?>> propertyExpression,
            MappingSignificance significance = MappingSignificance.Required)
        {
            var jsonValueMapping = new JsonValueMapping<Guid>
            {
                ValueKind = JsonValueMappingKind.Guid,
                Significance = MappingSignificance.Required
            };

            var jsonArrayMapping = new JsonArrayMapping<Guid>
            {
                ValueMapping = jsonValueMapping,
                Significance = MappingSignificance.Required
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
        
        public void Map(
            string propertyName,
            Expression<Func<TObject, IEnumerable<Guid?>?>> propertyExpression,
            MappingSignificance significance = MappingSignificance.Required)
        {
            var jsonValueMapping = new JsonValueMapping<Guid?>
            {
                ValueKind = JsonValueMappingKind.Guid,
                Significance = MappingSignificance.Optional
            };

            var jsonArrayMapping = new JsonArrayMapping<Guid?>()
            {
                ValueMapping = jsonValueMapping,
                Significance = MappingSignificance.Optional
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
