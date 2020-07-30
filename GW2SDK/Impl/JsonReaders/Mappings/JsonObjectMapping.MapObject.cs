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
                Significance = significance
            };
            var jsonPropertyMapping = new JsonPropertyMapping
            {
                Name = propertyName,
                Significance = significance,
                ValueNode = jsonObjectMapping,
                ParentNode = this
            };
            jsonObjectMapping.ParentNode = jsonPropertyMapping;
            map(jsonObjectMapping);
            Children.Add(jsonPropertyMapping);
        }

        public void Map<TProperty>(
            string propertyName,
            Expression<Func<TObject, TProperty>> propertyExpression,
            Action<JsonObjectMapping<TProperty>> map,
            MappingSignificance significance = MappingSignificance.Required)
        {
            var jsonObjectMapping = new JsonObjectMapping<TProperty>
            {
                Name = propertyName,
                Significance = significance
            };

            var jsonPropertyMapping = new JsonPropertyMapping
            {
                Name = propertyName,
                Destination = ((MemberExpression) propertyExpression.Body).Member,
                Significance = significance,
                ValueNode = jsonObjectMapping,
                ParentNode = this
            };

            jsonObjectMapping.ParentNode = jsonPropertyMapping;
            map(jsonObjectMapping);
            Children.Add(jsonPropertyMapping);
        }
    }
}
