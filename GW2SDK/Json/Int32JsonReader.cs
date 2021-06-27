using System.Text.Json;

namespace GW2SDK.Json
{
    internal sealed class Int32JsonReader : IJsonReader<int>
    {
        public int Read(JsonElement json, MissingMemberBehavior missingMemberBehavior) => json.GetInt32();
    }
}
