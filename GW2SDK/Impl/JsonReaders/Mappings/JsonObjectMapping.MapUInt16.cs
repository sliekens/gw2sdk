using System;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map(string propertyName, Expression<Func<TObject, ushort>> uint16)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) uint16.Body).Member,
                    Significance = MappingSignificance.Required,
                    ValueNode = new JsonValueMapping<ushort>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.UInt16,
                        Significance = MappingSignificance.Required
                    }
                }
            );
        }

        public void Map(string propertyName, Expression<Func<TObject, ushort?>> uint16)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) uint16.Body).Member,
                    Significance = MappingSignificance.Optional,
                    ValueNode = new JsonValueMapping<ushort?>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.UInt16,
                        Significance = MappingSignificance.Optional
                    }
                }
            );
        }
    }
}
