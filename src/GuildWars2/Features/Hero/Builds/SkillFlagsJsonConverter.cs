using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal sealed class SkillFlagsJsonConverter : JsonConverter<SkillFlags>
{
    public override SkillFlags Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, SkillFlags value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static SkillFlags Read(in JsonElement json)
    {
        JsonElement otherJson = json.GetProperty("other");
        JsonElement groundTargetedJson = json.GetProperty("ground_targeted");
        JsonElement noUnderwaterJson = json.GetProperty("no_underwater");
        return new SkillFlags
        {
            Other = otherJson.GetList(static (in value) => value.GetStringRequired()),
            GroundTargeted = groundTargetedJson.GetBoolean(),
            NoUnderwater = noUnderwaterJson.GetBoolean()
        };
    }

    public static void Write(Utf8JsonWriter writer, SkillFlags value)
    {
        writer.WriteStartObject();
        writer.WriteStartArray("other");
        foreach (string item in value.Other)
        {
            writer.WriteStringValue(item);
        }
        writer.WriteEndArray();
        writer.WriteBoolean("ground_targeted", value.GroundTargeted);
        writer.WriteBoolean("no_underwater", value.NoUnderwater);
        writer.WriteEndObject();
    }
}
