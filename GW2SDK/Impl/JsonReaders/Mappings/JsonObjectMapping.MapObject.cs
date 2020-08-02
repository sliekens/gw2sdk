using System;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map(string propertyName, Action<JsonDeconstructionMapping<TObject>> map, MappingSignificance significance = MappingSignificance.Required)
        {
            var jsonObjectMapping = new JsonDeconstructionMapping<TObject>
            {
                Name = propertyName,
                Significance = significance
            };
            map(jsonObjectMapping);
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Significance = significance,
                    ValueNode = jsonObjectMapping
                }
            );
        }

        public void Map<TProperty>(
            string propertyName,
            Expression<Func<TObject, TProperty>> @object,
            Action<JsonObjectMapping<TProperty>> map,
            MappingSignificance significance = MappingSignificance.Required)
        {
            var jsonObjectMapping = new JsonObjectMapping<TProperty>
            {
                Name = propertyName,
                Significance = significance
            };
            map(jsonObjectMapping);
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) @object.Body).Member,
                    Significance = significance,
                    ValueNode = jsonObjectMapping
                }
            );
        }
    }
}
