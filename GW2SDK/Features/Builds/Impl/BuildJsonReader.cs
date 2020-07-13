using System.Text.Json;
using GW2SDK.Impl.JsonReaders;

namespace GW2SDK.Builds.Impl
{
    internal sealed class BuildJsonReader : IJsonReader<Build>
    {
        public Build Read(in JsonElement json)
        {
            if (json.ValueKind != JsonValueKind.Object)
            {
                throw new JsonException("JSON is not an object.");
            }

            int id = default;
            bool idSeen = default;
            var typeName = nameof(Build);
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("id"))
                {
                    idSeen = true;
                    id = member.Value.GetInt32();
                }
                else
                {
                    throw new JsonException($"Could not find member '{member.Name}' on object of type '{typeName}'.");
                }
            }

            if (!idSeen)
            {
                throw new JsonException($"Missing required property 'id' for object of type '{typeName}'.");
            }

            return new Build { Id = id };
        }
    }
}
