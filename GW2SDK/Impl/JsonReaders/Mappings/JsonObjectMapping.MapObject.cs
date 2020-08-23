﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public partial class JsonObjectMapping<TObject>
    {
        public void Map(string propertyName, MapProperty<TObject> map, MappingSignificance significance = MappingSignificance.Required)
        {
            var jsonObjectMapping = new JsonObjectMapping<TObject>
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
            Expression<SelectProperty<TObject, TProperty>> @object,
            MapProperty<TProperty> map,
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

        public void Map<TProperty>(
            string propertyName,
            Expression<SelectProperty<TObject, IEnumerable<TProperty>?>> @object,
            MapProperty<TProperty> map,
            MappingSignificance significance = MappingSignificance.Required,
            MappingSignificance itemSignificance = MappingSignificance.Required)
        {
            var jsonObjectMapping = new JsonObjectMapping<TProperty>
            {
                Name = propertyName,
                Significance = itemSignificance
            };

            map(jsonObjectMapping);

            Children.Add(
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Destination = ((MemberExpression) @object.Body).Member,
                    Significance = significance,
                    ValueNode = new JsonArrayMapping<TProperty>
                    {
                        Name = propertyName,
                        ValueMapping = jsonObjectMapping,
                        Significance = significance
                    }
                }
            );
        }
    }
}
