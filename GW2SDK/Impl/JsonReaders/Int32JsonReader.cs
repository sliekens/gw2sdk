using System.Text.Json;

namespace GW2SDK.Impl.JsonReaders
{
    internal sealed class Int32JsonReader : IJsonReader<int>
    {
        public int Read(in JsonElement json, in JsonPath _) => json.GetInt32();

        public bool CanRead(in JsonElement json) => json.ValueKind == JsonValueKind.Number && json.TryGetInt32(out _);
    }
}
