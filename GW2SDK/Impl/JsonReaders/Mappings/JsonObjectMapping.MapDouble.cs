using System;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map(string propertyName, Expression<SelectProperty<TObject, double>> @double)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) @double.Body).Member,
                    Significance = MappingSignificance.Required,
                    ValueNode = new JsonValueMapping<double>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.Double,
                        Significance = MappingSignificance.Required
                    }
                }
            );
        }

        public void Map(string propertyName, Expression<SelectProperty<TObject, double?>> @double)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) @double.Body).Member,
                    Significance = MappingSignificance.Optional,
                    ValueNode = new JsonValueMapping<double?>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.Double,
                        Significance = MappingSignificance.Optional
                    }
                }
            );
        }
    }
}
