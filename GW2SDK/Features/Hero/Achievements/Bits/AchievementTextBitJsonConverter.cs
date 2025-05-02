using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Bits;

internal sealed class AchievementTextBitJsonConverter : JsonConverter<AchievementTextBit>
{
    public const string DiscriminatorValue = "text";

    public override AchievementTextBit Read(
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
        AchievementTextBit value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static AchievementTextBit Read(JsonElement json)
    {
        return new AchievementTextBit { Text = json.GetProperty("text").GetStringRequired() };
    }

    public static void Write(Utf8JsonWriter writer, AchievementTextBit value)
    {
        writer.WriteStartObject();
        writer.WriteString(AchievementBitJsonConverter.DiscriminatorName, DiscriminatorValue);
        writer.WriteString("text", value.Text);
        writer.WriteEndObject();
    }
}
