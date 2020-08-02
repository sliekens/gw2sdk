using System;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map(string propertyName, Expression<Func<TObject, float>> single)
        {
            Children.Add(new JsonPropertyMapping
            {
                Name = propertyName,
                Destination = ((MemberExpression) single.Body).Member,
                Significance = MappingSignificance.Required,
                ValueNode = new JsonValueMapping<float>
                {
                    ValueKind = JsonValueMappingKind.Single,
                    Significance = MappingSignificance.Required
                }
            });
        }

        public void Map(string propertyName, Expression<Func<TObject, float?>> single)
        {
            Children.Add(new JsonPropertyMapping
            {
                Name = propertyName,
                Destination = ((MemberExpression) single.Body).Member,
                Significance = MappingSignificance.Optional,
                ValueNode = new JsonValueMapping<float?>
                {
                    ValueKind = JsonValueMappingKind.Single,
                    Significance = MappingSignificance.Optional
                }
            });
        }
    }
}
