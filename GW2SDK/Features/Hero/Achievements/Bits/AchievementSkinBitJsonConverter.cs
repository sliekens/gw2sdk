using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements.Bits;

internal sealed class AchievementSkinBitJsonConverter : JsonConverter<AchievementSkinBit>
{
    public const string DiscriminatorValue = "skin";

    public override AchievementSkinBit Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, AchievementSkinBit value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static AchievementSkinBit Read(JsonElement json)
    {
        return new AchievementSkinBit
        {
            Id = json.GetProperty("id").GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, AchievementSkinBit value)
    {
        writer.WriteStartObject();
        writer.WriteString(AchievementBitJsonConverter.DiscriminatorName, DiscriminatorValue);
        writer.WriteNumber("id", value.Id);
        writer.WriteEndObject();
    }
}
