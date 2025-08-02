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

    public override void Write(
        Utf8JsonWriter writer,
        AchievementSkinBit value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static AchievementSkinBit Read(in JsonElement json)
    {
        JsonElement id = default, text = default;
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("id"))
            {
                id = member.Value;
            }
            else if (member.NameEquals("text"))
            {
                text = member.Value;
            }
        }

        return new AchievementSkinBit
        {
            Id = id.GetInt32(),
            Text = text.ValueKind == JsonValueKind.String
                ? text.GetString() ?? ""
                : ""
        };
    }

    public static void Write(Utf8JsonWriter writer, AchievementSkinBit value)
    {
        writer.WriteStartObject();
        writer.WriteString(AchievementBitJsonConverter.DiscriminatorName, DiscriminatorValue);
        AchievementBitJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteNumber("id", value.Id);
        writer.WriteEndObject();
    }
}
