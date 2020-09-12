using System;
using System.Text.Json;

namespace GW2SDK.Impl.JsonReaders.Mappings
{
    public delegate object ConvertJsonElement(in JsonElement jsonElement, in JsonPath jsonPath);

    public delegate T ConvertJsonElement<out T>(in JsonElement jsonElement, in JsonPath jsonPath);

    public class JsonValueMapping<TValue> : JsonMapping, IJsonValueMapping
    {
        public JsonValueMappingKind ValueKind { get; set; }

        public Type ValueType { get; } = typeof(TValue);

        public object ConvertJsonElement(in JsonElement element, in JsonPath path) => JsonConverter!(in element, in path)!;

        public ConvertJsonElement<TValue>? JsonConverter { get; set; }

        public override void Accept(IJsonMappingVisitor visitor)
        {
            visitor.VisitValue(this);
        }
    }
}
