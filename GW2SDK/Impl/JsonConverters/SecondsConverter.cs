using System;
using Newtonsoft.Json;

namespace GW2SDK.Impl.JsonConverters
{
    internal sealed class SecondsConverter : JsonConverter<TimeSpan>
    {
        public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer) => writer.WriteValue((int)value.TotalSeconds);

        public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, JsonSerializer serializer) =>
            TimeSpan.FromSeconds(Convert.ToDouble(reader.Value));
    }
}
