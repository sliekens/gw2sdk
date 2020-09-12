using System;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public class JsonArrayMapping<TValue> : JsonMapping, IJsonArrayMapping
    {
        public Type ValueType { get; } = typeof(TValue);

        public IJsonMapping ValueMapping { get; set; } = default!;

        public override void Accept(IJsonMappingVisitor visitor) => visitor.VisitArray(this);
    }
}
