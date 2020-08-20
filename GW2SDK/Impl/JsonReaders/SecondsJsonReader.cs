using System;
using System.Text.Json;

namespace GW2SDK.Impl.JsonReaders
{
    internal class SecondsJsonReader : IJsonReader<TimeSpan>
    {
        private SecondsJsonReader()
        {
        }

        public static IJsonReader<TimeSpan> Instance { get; } = new SecondsJsonReader();

        public TimeSpan Read(in JsonElement json, in JsonPath _) => TimeSpan.FromSeconds(json.GetDouble());

        public bool CanRead(in JsonElement json) => json.ValueKind == JsonValueKind.Number;
    }
}
