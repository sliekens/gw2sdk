using System;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map(string propertyName, Expression<Func<TObject, ushort>> uint16)
        {
            var jsonValueMapping = new JsonValueMapping<ushort>
            {
                ValueKind = JsonValueMappingKind.UInt16,
                Significance = MappingSignificance.Required
            };

            var jsonPropertyMapping = new JsonPropertyMapping
            {
                Name = propertyName,
                Destination = ((MemberExpression) uint16.Body).Member,
                Significance = MappingSignificance.Required,
                ValueNode = jsonValueMapping,
                ParentNode = this
            };

            jsonValueMapping.ParentNode = jsonPropertyMapping;
            Children.Add(jsonPropertyMapping);
        }

        public void Map(string propertyName, Expression<Func<TObject, ushort?>> uint16)
        {
            var jsonValueMapping = new JsonValueMapping<ushort?>
            {
                ValueKind = JsonValueMappingKind.UInt16,
                Significance = MappingSignificance.Optional
            };

            var jsonPropertyMapping = new JsonPropertyMapping
            {
                Name = propertyName,
                Destination = ((MemberExpression) uint16.Body).Member,
                Significance = MappingSignificance.Optional,
                ValueNode = jsonValueMapping,
                ParentNode = this
            };

            jsonValueMapping.ParentNode = jsonPropertyMapping;
            Children.Add(jsonPropertyMapping);
        }
    }
}
