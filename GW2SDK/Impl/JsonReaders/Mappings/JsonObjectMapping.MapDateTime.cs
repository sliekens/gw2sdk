using System;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map(string propertyName, Expression<SelectProperty<TObject, DateTime>> dateTime)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) dateTime.Body).Member,
                    Significance = MappingSignificance.Required,
                    ValueNode = new JsonValueMapping<DateTime>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.DateTime,
                        Significance = MappingSignificance.Required
                    }
                }
            );
        }

        public void Map(string propertyName, Expression<SelectProperty<TObject, DateTime?>> dateTime)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) dateTime.Body).Member,
                    Significance = MappingSignificance.Optional,
                    ValueNode = new JsonValueMapping<DateTime?>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.DateTime,
                        Significance = MappingSignificance.Optional
                    }
                }
            );
        }
    }
}
