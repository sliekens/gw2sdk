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

    public static AchievementItemBit Read(in JsonElement json)
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

        return new AchievementItemBit
        {
            Id = id.GetInt32(),
            Text = text.ValueKind == JsonValueKind.String
                ? text.GetString() ?? ""
                : ""
        };
    }

    public static void Write(Utf8JsonWriter writer, AchievementItemBit value)
    {
        writer.WriteStartObject();
        writer.WriteString(AchievementBitJsonConverter.DiscriminatorName, DiscriminatorValue);
        AchievementBitJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteNumber("id", value.Id);
        writer.WriteEndObject();
    }
}
