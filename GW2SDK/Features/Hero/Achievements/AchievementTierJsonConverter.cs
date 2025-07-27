using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements;

internal sealed class AchievementTierJsonConverter : JsonConverter<AchievementTier>
{
    public override AchievementTier Read(
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
        AchievementTier value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static AchievementTier Read(in JsonElement json)
    {
        return new AchievementTier
        {
            Count = json.GetProperty("count").GetInt32(),
            Points = json.GetProperty("points").GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, AchievementTier value)
    {
        writer.WriteStartObject();
        writer.WriteNumber("count", value.Count);
        writer.WriteNumber("points", value.Points);
        writer.WriteEndObject();
    }
}
