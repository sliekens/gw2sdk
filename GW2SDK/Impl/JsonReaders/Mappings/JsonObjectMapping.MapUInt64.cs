using System;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map(string propertyName, Expression<Func<TObject, ulong>> uint64)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) uint64.Body).Member,
                    Significance = MappingSignificance.Required,
                    ValueNode = new JsonValueMapping<ulong>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.UInt64,
                        Significance = MappingSignificance.Required
                    }
                }
            );
        }

        public void Map(string propertyName, Expression<Func<TObject, ulong?>> uint64)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) uint64.Body).Member,
                    Significance = MappingSignificance.Optional,
                    ValueNode = new JsonValueMapping<ulong?>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.UInt64,
                        Significance = MappingSignificance.Optional
                    }
                }
            );
        }
    }
}
