using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map<TProperty>(
            string propertyName,
            Expression<Func<TObject, TProperty>> custom,
            IJsonReader<TProperty> jsonReader,
            MappingSignificance significance = MappingSignificance.Required)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) custom.Body).Member,
                    Significance = significance,
                    ValueNode = new JsonValueMapping<TProperty>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.Custom,
                        JsonReader = jsonReader,
                        Significance = significance
                    }
                }
            );
        }

        public void Map<TProperty>(
            string propertyName,
            Expression<Func<TObject, IEnumerable<TProperty>?>> custom,
            IJsonReader<TProperty> jsonReader,
            MappingSignificance significance = MappingSignificance.Required,
            MappingSignificance itemSignificance = MappingSignificance.Required)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) custom.Body).Member,
                    Significance = significance,
                    ValueNode = new JsonArrayMapping<TProperty>
                    {
                        Name = propertyName,
                        ValueMapping = new JsonValueMapping<TProperty>
                        {
                            Name = propertyName,
                            ValueKind = JsonValueMappingKind.Custom,
                            JsonReader = jsonReader,
                            Significance = itemSignificance
                        },
                        Significance = significance
                    }
                }
            );
        }
    }
}
