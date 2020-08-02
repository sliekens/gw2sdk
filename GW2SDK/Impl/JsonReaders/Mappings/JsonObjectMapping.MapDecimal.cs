using System;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map(string propertyName, Expression<Func<TObject, decimal>> @decimal)
        {
            Children.Add(new JsonPropertyMapping
            {
                Name = propertyName,
                Destination = ((MemberExpression) @decimal.Body).Member,
                Significance = MappingSignificance.Required,
                ValueNode = new JsonValueMapping<decimal>
                {
                    ValueKind = JsonValueMappingKind.Decimal,
                    Significance = MappingSignificance.Required
                }
            });
        }

        public void Map(string propertyName, Expression<Func<TObject, decimal?>> @decimal)
        {
            Children.Add(new JsonPropertyMapping
            {
                Name = propertyName,
                Destination = ((MemberExpression) @decimal.Body).Member,
                Significance = MappingSignificance.Optional,
                ValueNode = new JsonValueMapping<decimal?>
                {
                    ValueKind = JsonValueMappingKind.Decimal,
                    Significance = MappingSignificance.Optional
                }
            });
        }
    }
}
