using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Hero.Achievements.Bits;

namespace GuildWars2.Hero.Achievements;

internal sealed class AchievementBitJsonConverter : JsonConverter<AchievementBit>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "bit";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(AchievementBit).IsAssignableFrom(typeToConvert);
    }

    public override AchievementBit Read(
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
        AchievementBit value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static AchievementBit Read(in JsonElement json)
    {
        if (json.TryGetProperty(DiscriminatorName, out var discriminator))
        {
            switch (discriminator.GetString())
            {
                case AchievementItemBitJsonConverter.DiscriminatorValue:
                    return AchievementItemBitJsonConverter.Read(json);
                case AchievementMiniatureBitJsonConverter.DiscriminatorValue:
                    return AchievementMiniatureBitJsonConverter.Read(json);
                case AchievementSkinBitJsonConverter.DiscriminatorValue:
                    return AchievementSkinBitJsonConverter.Read(json);
                case AchievementTextBitJsonConverter.DiscriminatorValue:
                    return AchievementTextBitJsonConverter.Read(json);
            }
        }

        return new AchievementBit();
    }

    public static void Write(Utf8JsonWriter writer, AchievementBit value)
    {
        switch (value)
        {
            case AchievementItemBit itemBit:
                AchievementItemBitJsonConverter.Write(writer, itemBit);
                break;
            case AchievementMiniatureBit miniatureBit:
                AchievementMiniatureBitJsonConverter.Write(writer, miniatureBit);
                break;
            case AchievementSkinBit skinBit:
                AchievementSkinBitJsonConverter.Write(writer, skinBit);
                break;
            case AchievementTextBit textBit:
                AchievementTextBitJsonConverter.Write(writer, textBit);
                break;
            default:
                writer.WriteStartObject();
                writer.WriteString(DiscriminatorName, DiscriminatorValue);
                writer.WriteEndObject();
                break;
        }
    }
}
