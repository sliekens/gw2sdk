using System.Text.Json;

namespace GW2SDK.Impl.JsonReaders
{
    internal sealed class StringJsonReader : IJsonReader<string>
    {
        public string Read(in JsonElement json) => json.GetString();
    }
}