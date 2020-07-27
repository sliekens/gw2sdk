using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public class JsonObjectMapping<TValue> : JsonMapping
    {
        public override JsonMappingKind Kind => JsonMappingKind.Object;

        public List<JsonMapping> Children { get; } = new List<JsonMapping>();

        public override void Accept(IJsonMappingVisitor visitor)
        {
            visitor.VisitObject(this);
        }

        public void Map<TObject>(
            string propertyName,
            Expression<Func<TValue, TObject>> propertyExpression,
            IJsonReader<TObject> jsonReader,
            MappingSignificance significance = MappingSignificance.Required)
        {
            Children.Add(
                new JsonValueMapping<TObject>
                {
                    Name = propertyName,
                    ValueKind = JsonValueMappingKind.Custom,
                    JsonReader = jsonReader,
                    Significance = significance
                }
            );
        }

        public void Map(string propertyName, Expression<Func<TValue, int>> propertyExpression)
        {
            Children.Add(
                new JsonValueMapping<int>
                {
                    Name = propertyName,
                    ValueKind = JsonValueMappingKind.Int32,
                    Significance = MappingSignificance.Required
                }
            );
        }

        public void Map(string propertyName, Expression<Func<TValue, int?>> propertyExpression)
        {
            Children.Add(
                new JsonValueMapping<int?>
                {
                    Name = propertyName,
                    ValueKind = JsonValueMappingKind.Int32,
                    Significance = MappingSignificance.Optional
                }
            );
        }

        public void Map(string propertyName, Action<JsonObjectMapping<TValue>> map, MappingSignificance significance = MappingSignificance.Required)
        {
            var child = new JsonObjectMapping<TValue>
            {
                Name = propertyName,
                Significance = significance
            };
            map(child);
            Children.Add(child);
        }
    }
}