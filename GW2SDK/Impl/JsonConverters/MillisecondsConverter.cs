using System;
using Newtonsoft.Json;

namespace GW2SDK.Impl.JsonConverters
{
    internal sealed class MillisecondsConverter : JsonConverter<TimeSpan>
    {
        public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer) => writer.WriteValue((int)value.TotalMilliseconds);

        public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, JsonSerializer serializer) =>
            TimeSpan.FromMilliseconds(Convert.ToDouble(reader.Value));
    }
}
