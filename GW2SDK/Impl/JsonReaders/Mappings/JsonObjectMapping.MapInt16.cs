using System;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map(string propertyName, Expression<SelectProperty<TObject, short>> int16)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) int16.Body).Member,
                    Significance = MappingSignificance.Required,
                    ValueNode = new JsonValueMapping<short>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.Int16,
                        Significance = MappingSignificance.Required
                    }
                }
            );
        }

        public void Map(string propertyName, Expression<SelectProperty<TObject, short?>> int16)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) int16.Body).Member,
                    Significance = MappingSignificance.Optional,
                    ValueNode = new JsonValueMapping<short?>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.Int16,
                        Significance = MappingSignificance.Optional
                    }
                }
            );
        }
    }
}
