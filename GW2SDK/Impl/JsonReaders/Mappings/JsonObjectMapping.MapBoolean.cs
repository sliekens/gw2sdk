using System;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map(string propertyName, Expression<SelectProperty<TObject, bool>> boolean)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) boolean.Body).Member,
                    Significance = MappingSignificance.Required,
                    ValueNode = new JsonValueMapping<bool>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.Boolean,
                        Significance = MappingSignificance.Required
                    }
                }
            );
        }

        public void Map(string propertyName, Expression<SelectProperty<TObject, bool?>> boolean)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) boolean.Body).Member,
                    Significance = MappingSignificance.Optional,
                    ValueNode = new JsonValueMapping<bool?>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.Boolean,
                        Significance = MappingSignificance.Optional
                    }
                }
            );
        }
    }
}
