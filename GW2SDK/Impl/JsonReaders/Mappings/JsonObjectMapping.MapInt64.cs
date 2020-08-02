using System;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map(string propertyName, Expression<Func<TObject, long>> int64)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) int64.Body).Member,
                    Significance = MappingSignificance.Required,
                    ValueNode = new JsonValueMapping<long>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.Int64,
                        Significance = MappingSignificance.Required
                    }
                }
            );
        }

        public void Map(string propertyName, Expression<Func<TObject, long?>> int64)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) int64.Body).Member,
                    Significance = MappingSignificance.Optional,
                    ValueNode = new JsonValueMapping<long?>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.Int64,
                        Significance = MappingSignificance.Optional
                    }
                }
            );
        }
    }
}
