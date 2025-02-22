using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements.Bits;

internal sealed class AchievementMiniatureBitJsonConverter : JsonConverter<AchievementMiniatureBit>
{
    public const string DiscriminatorValue = "miniature";

    public override AchievementMiniatureBit Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, AchievementMiniatureBit value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static AchievementMiniatureBit Read(JsonElement json)
    {
        return new AchievementMiniatureBit
        {
            Id = json.GetProperty("id").GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, AchievementMiniatureBit value)
    {
        writer.WriteStartObject();
        writer.WriteString(AchievementBitJsonConverter.DiscriminatorName, DiscriminatorValue);
        writer.WriteNumber("id", value.Id);
        writer.WriteEndObject();
    }
}
