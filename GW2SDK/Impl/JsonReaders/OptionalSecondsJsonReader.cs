using System;
using System.Text.Json;

namespace GW2SDK.Impl.JsonReaders
{
    internal class OptionalSecondsJsonReader : IJsonReader<TimeSpan?>
    {
        private OptionalSecondsJsonReader()
        {
        }

        public static IJsonReader<TimeSpan?> Instance { get; } = new OptionalSecondsJsonReader();

        public TimeSpan? Read(in JsonElement json) => json.ValueKind == JsonValueKind.Null ? (TimeSpan?) null : TimeSpan.FromSeconds(json.GetDouble());

        public bool CanRead(in JsonElement json) => json.ValueKind == JsonValueKind.Number;
    }
}
