using System.Text.Json;

namespace GW2SDK.Impl.JsonReaders
{
    internal sealed class Int32JsonReader : IJsonReader<int>
    {
        public int Read(in JsonElement json) => json.GetInt32();
    }
}
