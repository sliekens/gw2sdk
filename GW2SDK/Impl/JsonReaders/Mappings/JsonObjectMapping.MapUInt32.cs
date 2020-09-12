using System;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map(string propertyName, Expression<SelectProperty<TObject, uint>> uint32)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) uint32.Body).Member,
                    Significance = MappingSignificance.Required,
                    ValueMapping = new JsonValueMapping<uint>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.UInt32,
                        Significance = MappingSignificance.Required
                    }
                }
            );
        }

        public void Map(string propertyName, Expression<SelectProperty<TObject, uint?>> uint32)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) uint32.Body).Member,
                    Significance = MappingSignificance.Optional,
                    ValueMapping = new JsonValueMapping<uint?>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.UInt32,
                        Significance = MappingSignificance.Optional
                    }
                }
            );
        }
    }
}
