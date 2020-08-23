using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map(
            string propertyName,
            Expression<SelectProperty<TObject, string?>> @string,
            MappingSignificance significance = MappingSignificance.Required)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) @string.Body).Member,
                    Significance = significance,
                    ValueNode = new JsonValueMapping<string>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.String,
                        Significance = significance
                    }
                }
            );
        }

        public void Map(
            string propertyName,
            Expression<SelectProperty<TObject, IEnumerable<string?>?>> strings,
            MappingSignificance significance = MappingSignificance.Required,
            MappingSignificance itemSignificance = MappingSignificance.Required)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) strings.Body).Member,
                    Significance = significance,
                    ValueNode = new JsonArrayMapping<string>
                    {
                        ValueMapping = new JsonValueMapping<string>
                        {
                            Name = propertyName,
                            ValueKind = JsonValueMappingKind.String,
                            Significance = itemSignificance
                        },
                        Significance = significance
                    }
                }
            );
        }
    }
}
