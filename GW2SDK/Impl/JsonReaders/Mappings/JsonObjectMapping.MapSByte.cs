using System;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map(string propertyName, Expression<SelectProperty<TObject, sbyte>> @sbyte)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) @sbyte.Body).Member,
                    Significance = MappingSignificance.Required,
                    ValueNode = new JsonValueMapping<sbyte>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.SByte,
                        Significance = MappingSignificance.Required
                    }
                }
            );
        }

        public void Map(string propertyName, Expression<SelectProperty<TObject, sbyte?>> @sbyte)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) @sbyte.Body).Member,
                    Significance = MappingSignificance.Optional,
                    ValueNode = new JsonValueMapping<sbyte?>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.SByte,
                        Significance = MappingSignificance.Optional
                    }
                }
            );
        }
    }
}
