using System.Text.Json;

namespace GW2SDK.Json
{
    internal sealed class StringJsonReader : IJsonReader<string>
    {
        public string Read(JsonElement json, MissingMemberBehavior missingMemberBehavior) => json.GetStringRequired();
    }
}
