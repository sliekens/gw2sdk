using System;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map(string propertyName, Expression<Func<TObject, byte>> @byte)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) @byte.Body).Member,
                    Significance = MappingSignificance.Required,
                    ValueNode = new JsonValueMapping<byte>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.Byte,
                        Significance = MappingSignificance.Required
                    }
                }
            );
        }

        public void Map(string propertyName, Expression<Func<TObject, byte?>> @byte)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) @byte.Body).Member,
                    Significance = MappingSignificance.Optional,
                    ValueNode = new JsonValueMapping<byte?>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.Byte,
                        Significance = MappingSignificance.Optional
                    }
                }
            );
        }
    }
}
