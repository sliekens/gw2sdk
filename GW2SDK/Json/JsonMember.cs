using System.Text.Json;

namespace GW2SDK.Json
{
    internal readonly ref struct JsonMember
    {
        internal JsonMember(JsonElement value)
        {
            Value = value;
            Visited = true;
        }

        private bool Visited { get; }

        internal JsonElement Value { get; }

        internal bool IsMissing => !Visited || Value.ValueKind == JsonValueKind.Null;
    }
}
