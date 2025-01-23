using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Items;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal sealed class GatheringToolSkinJsonConverter : JsonConverter<GatheringToolSkin>
{
    public const string DiscriminatorValue = "gathering_tool_skin";

    public const string DiscriminatorName = "$gathering_tool_skin_type";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(GatheringToolSkin).IsAssignableFrom(typeToConvert);
    }

    public override GatheringToolSkin Read(
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
        GatheringToolSkin value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static GatheringToolSkin Read(JsonElement json)
    {
        if (!json.GetProperty(EquipmentSkinJsonConverter.DiscriminatorName)
            .ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(EquipmentSkinJsonConverter.DiscriminatorName).GetString()
            );
        }

        if (json.TryGetProperty(DiscriminatorName, out var discriminator))
        {
            switch (discriminator.ToString())
            {
                case FishingToolSkinJsonConverter.DiscriminatorValue:
                    return FishingToolSkinJsonConverter.Read(json);
                case ForagingToolSkinJsonConverter.DiscriminatorValue:
                    return ForagingToolSkinJsonConverter.Read(json);
                case LoggingToolSkinJsonConverter.DiscriminatorValue:
                    return LoggingToolSkinJsonConverter.Read(json);
                case MiningToolSkinJsonConverter.DiscriminatorValue:
                    return MiningToolSkinJsonConverter.Read(json);
            }
        }

        return new GatheringToolSkin
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Flags = SkinFlagsJsonConverter.Read(json.GetProperty("flags")),
            Races = json.GetProperty("races").GetList(static value => value.GetEnum<RaceName>()),
            Rarity = json.GetProperty("rarity").GetEnum<Rarity>(),
            IconHref = json.GetProperty("icon").GetString()
        };
    }

    public static void Write(Utf8JsonWriter writer, GatheringToolSkin value)
    {
        switch (value)
        {
            case FishingToolSkin fishingToolSkin:
                FishingToolSkinJsonConverter.Write(writer, fishingToolSkin);
                break;
            case ForagingToolSkin foragingToolSkin:
                ForagingToolSkinJsonConverter.Write(writer, foragingToolSkin);
                break;
            case LoggingToolSkin loggingToolSkin:
                LoggingToolSkinJsonConverter.Write(writer, loggingToolSkin);
                break;
            case MiningToolSkin miningToolSkin:
                MiningToolSkinJsonConverter.Write(writer, miningToolSkin);
                break;
            default:
                writer.WriteStartObject();
                writer.WriteString(
                    EquipmentSkinJsonConverter.DiscriminatorName,
                    DiscriminatorValue
                );
                writer.WriteString(DiscriminatorName, DiscriminatorValue);
                WriteCommonProperties(writer, value);
                writer.WriteEndObject();
                break;
        }
    }

    public static void WriteCommonProperties(Utf8JsonWriter writer, GatheringToolSkin value)
    {
        EquipmentSkinJsonConverter.WriteCommonProperties(writer, value);
    }
}
