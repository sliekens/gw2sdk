using System;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TValue>
    {
        public void Map(string propertyName, Expression<Func<TValue, DateTimeOffset>> propertyExpression)
        {
            var jsonValueMapping = new JsonValueMapping<DateTimeOffset>
            {
                ValueKind = JsonValueMappingKind.DateTimeOffset,
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

        public void Map(string propertyName, Expression<Func<TValue, DateTimeOffset?>> propertyExpression)
        {
            var jsonValueMapping = new JsonValueMapping<DateTimeOffset?>
            {
                ValueKind = JsonValueMappingKind.DateTimeOffset,
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

            jsonValueMapping.ParentNode = jsonValueMapping;
            Children.Add(jsonPropertyMapping);
        }
    }
}
