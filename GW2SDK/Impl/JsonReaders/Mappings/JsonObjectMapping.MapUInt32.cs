using System;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map(string propertyName, Expression<Func<TObject, uint>> uint32)
        {
            Children.Add(new JsonPropertyMapping
            {
                Name = propertyName,
                Destination = ((MemberExpression) uint32.Body).Member,
                Significance = MappingSignificance.Required,
                ValueNode = new JsonValueMapping<uint>
                {
                    ValueKind = JsonValueMappingKind.UInt32,
                    Significance = MappingSignificance.Required
                }
            });
        }

        public void Map(string propertyName, Expression<Func<TObject, uint?>> uint32)
        {
            Children.Add(new JsonPropertyMapping
            {
                Name = propertyName,
                Destination = ((MemberExpression) uint32.Body).Member,
                Significance = MappingSignificance.Optional,
                ValueNode = new JsonValueMapping<uint?>
                {
                    ValueKind = JsonValueMappingKind.UInt32,
                    Significance = MappingSignificance.Optional
                }
            });
        }
    }
}
