using System;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map(string propertyName, Expression<SelectProperty<TObject, DateTimeOffset>> dateTimeOffset)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) dateTimeOffset.Body).Member,
                    Significance = MappingSignificance.Required,
                    ValueNode = new JsonValueMapping<DateTimeOffset>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.DateTimeOffset,
                        Significance = MappingSignificance.Required
                    }
                }
            );
        }

        public void Map(string propertyName, Expression<SelectProperty<TObject, DateTimeOffset?>> dateTimeOffset)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) dateTimeOffset.Body).Member,
                    Significance = MappingSignificance.Optional,
                    ValueNode = new JsonValueMapping<DateTimeOffset?>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.DateTimeOffset,
                        Significance = MappingSignificance.Optional
                    }
                }
            );
        }
    }
}
