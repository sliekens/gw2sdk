using System;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map(string propertyName, Expression<Func<TObject, bool>> boolean)
        {
            var jsonValueMapping = new JsonValueMapping<bool>
            {
                ValueKind = JsonValueMappingKind.Boolean,
                Significance = MappingSignificance.Required
            };

            var jsonPropertyMapping = new JsonPropertyMapping
            {
                Name = propertyName,
                Destination = ((MemberExpression) boolean.Body).Member,
                Significance = MappingSignificance.Required,
                ValueNode = jsonValueMapping,
                ParentNode = this
            };

            jsonValueMapping.ParentNode = jsonPropertyMapping;
            Children.Add(jsonPropertyMapping);
        }

        public void Map(string propertyName, Expression<Func<TObject, bool?>> boolean)
        {
            var jsonValueMapping = new JsonValueMapping<bool?>
            {
                ValueKind = JsonValueMappingKind.Boolean,
                Significance = MappingSignificance.Optional
            };

            var jsonPropertyMapping = new JsonPropertyMapping
            {
                Name = propertyName,
                Destination = ((MemberExpression) boolean.Body).Member,
                Significance = MappingSignificance.Optional,
                ValueNode = jsonValueMapping,
                ParentNode = this
            };

            jsonValueMapping.ParentNode = jsonPropertyMapping;
            Children.Add(jsonPropertyMapping);
        }
    }
}
