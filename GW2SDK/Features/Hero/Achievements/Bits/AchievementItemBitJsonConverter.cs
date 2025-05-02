using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements.Bits;

internal sealed class AchievementItemBitJsonConverter : JsonConverter<AchievementItemBit>
{
    public const string DiscriminatorValue = "item_bit";

    public override AchievementItemBit Read(
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
        AchievementItemBit value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static AchievementItemBit Read(JsonElement json)
    {
        return new AchievementItemBit { Id = json.GetProperty("id").GetInt32() };
    }

    public static void Write(Utf8JsonWriter writer, AchievementItemBit value)
    {
        writer.WriteStartObject();
        writer.WriteString(AchievementBitJsonConverter.DiscriminatorName, DiscriminatorValue);
        writer.WriteNumber("id", value.Id);
        writer.WriteEndObject();
    }
}
