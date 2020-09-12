using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    [return: MaybeNull]
    public delegate TProperty SelectProperty<in TObject, out TProperty>(TObject clrType);

    public partial class JsonObjectMapping<TObject> : JsonMapping, IJsonObjectMapping
    {
        public Type ObjectType { get; } = typeof(TObject);

        public UnexpectedPropertyBehavior UnexpectedPropertyBehavior { get; set; } =
            UnexpectedPropertyBehavior.Error;

        public IJsonDiscriminatorMapping? Discriminator { get; private set; }

        public List<JsonPropertyMapping> Children { get; } = new List<JsonPropertyMapping>();

        public override void Accept(IJsonMappingVisitor visitor) => visitor.VisitObject(this);

        public void Ignore(string propertyName)
        {
            Children.Add
            (
                new JsonPropertyMapping
                {
                    Name = propertyName,
                    Significance = MappingSignificance.Ignored
                }
            );
        }
    }
}
