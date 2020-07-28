using System;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TValue>
    {
        public void Map(string propertyName, Expression<Func<TValue, int?>> propertyExpression)
        {
            var jsonValueMapping = new JsonValueMapping<int?>
            {
                ValueKind = JsonValueMappingKind.Int32,
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
