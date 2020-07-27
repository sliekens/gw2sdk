using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public class JsonAggregateMapping<TValue> : JsonMapping
    {
        public override JsonMappingKind Kind => JsonMappingKind.Aggregate;

        public List<JsonMapping> Children { get; } = new List<JsonMapping>();

        public override void Accept(IJsonMappingVisitor visitor) => visitor.VisitAggregate(this);

        public void Map(string propertyName, Expression<Func<TValue, int>> propertyExpression)
        {
            Children.Add(
                new JsonValueMapping<int>
                {
                    Significance = MappingSignificance.Required,
                    Name = propertyName,
                    ValueKind = JsonValueMappingKind.Int32,
                    Destination = ((MemberExpression) propertyExpression.Body).Member
                }
            );
        }
    }
}
