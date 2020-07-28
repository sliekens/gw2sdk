using System;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TValue>
    {
        public void Map(string propertyName, Action<JsonDeconstructionMapping<TValue>> map, MappingSignificance significance = MappingSignificance.Required)
        {
            var jsonObjectMapping = new JsonDeconstructionMapping<TValue>
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

        public void Map<TObject>(
            string propertyName,
            Expression<Func<TValue, TObject>> propertyExpression,
            Action<JsonObjectMapping<TObject>> map,
            MappingSignificance significance = MappingSignificance.Required)
        {
            var jsonObjectMapping = new JsonObjectMapping<TObject>
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
