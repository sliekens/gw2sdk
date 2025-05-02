using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Categories;

internal sealed class AchievementFlagsJsonConverter : JsonConverter<AchievementFlags>
{
    public override AchievementFlags Read(
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
        AchievementFlags value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static AchievementFlags Read(JsonElement json)
    {
        return new AchievementFlags
        {
            SpecialEvent = json.GetProperty("special_event").GetBoolean(),
            PvE = json.GetProperty("pve").GetBoolean(),
            Other = json.GetProperty("other").GetList(static value => value.GetStringRequired())
        };
    }

    public static void Write(Utf8JsonWriter writer, AchievementFlags value)
    {
        writer.WriteStartObject();
        writer.WriteBoolean("special_event", value.SpecialEvent);
        writer.WriteBoolean("pve", value.PvE);
        writer.WriteStartArray("other");
        foreach (var other in value.Other)
        {
            writer.WriteStringValue(other);
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
