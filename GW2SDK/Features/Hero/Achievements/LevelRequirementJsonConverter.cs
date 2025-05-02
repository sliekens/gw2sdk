using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements;

internal sealed class LevelRequirementJsonConverter : JsonConverter<LevelRequirement>
{
    public override LevelRequirement Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(
        Utf8JsonWriter writer,
        LevelRequirement value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static LevelRequirement Read(JsonElement json)
    {
        return new LevelRequirement
        {
            Min = json.GetProperty("min").GetInt32(),
            Max = json.GetProperty("max").GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, LevelRequirement value)
    {
        writer.WriteStartObject();
        writer.WriteNumber("min", value.Min);
        writer.WriteNumber("max", value.Max);
        writer.WriteEndObject();
    }
}
