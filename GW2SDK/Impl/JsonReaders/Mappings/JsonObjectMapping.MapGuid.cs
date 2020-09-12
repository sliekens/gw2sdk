using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map(string propertyName, Expression<SelectProperty<TObject, Guid>> guid)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) guid.Body).Member,
                    Significance = MappingSignificance.Required,
                    ValueMapping = new JsonValueMapping<Guid>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.Guid,
                        Significance = MappingSignificance.Required
                    }
                }
            );
        }

        public void Map(string propertyName, Expression<SelectProperty<TObject, Guid?>> guid)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) guid.Body).Member,
                    Significance = MappingSignificance.Optional,
                    ValueMapping = new JsonValueMapping<Guid?>
                    {
                        Name = propertyName,
                        ValueKind = JsonValueMappingKind.Guid,
                        Significance = MappingSignificance.Optional
                    }
                }
            );
        }

        public void Map(
            string propertyName,
            Expression<SelectProperty<TObject, IEnumerable<Guid>?>> guids,
            MappingSignificance significance = MappingSignificance.Required)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) guids.Body).Member,
                    Significance = significance,
                    ValueMapping = new JsonArrayMapping<Guid>
                    {
                        Name = propertyName,
                        ValueMapping = new JsonValueMapping<Guid>
                        {
                            Name = propertyName,
                            ValueKind = JsonValueMappingKind.Guid,
                            Significance = MappingSignificance.Required
                        },
                        Significance = significance
                    }
                }
            );
        }

        public void Map(
            string propertyName,
            Expression<Func<TObject, IEnumerable<Guid?>?>> guids,
            MappingSignificance significance = MappingSignificance.Required)
        {
            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) guids.Body).Member,
                    Significance = significance,
                    ValueMapping = new JsonArrayMapping<Guid?>
                    {
                        Name = propertyName,
                        ValueMapping = new JsonValueMapping<Guid?>
                        {
                            Name = propertyName,
                            ValueKind = JsonValueMappingKind.Guid,
                            Significance = MappingSignificance.Optional
                        },
                        Significance = significance
                    }
                }
            );
        }
    }
}
