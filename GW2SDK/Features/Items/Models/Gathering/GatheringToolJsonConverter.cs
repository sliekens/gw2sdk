using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class GatheringToolJsonConverter : JsonConverter<GatheringTool>
{
    public const string DiscriminatorValue = "gathering_tool";

    public const string DiscriminatorName = "$gathering_tool_type";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(GatheringTool).IsAssignableFrom(typeToConvert);
    }

    public override GatheringTool Read(
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
        GatheringTool value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static GatheringTool Read(in JsonElement json)
    {
        if (!json.GetProperty(ItemJsonConverter.DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(ItemJsonConverter.DiscriminatorName).GetString()
            );
        }

        if (json.TryGetProperty(DiscriminatorName, out var discriminator))
        {
            switch (discriminator.GetString())
            {
                case MiningPickJsonConverter.DiscriminatorValue:
                    return MiningPickJsonConverter.Read(json);
                case LoggingAxeJsonConverter.DiscriminatorValue:
                    return LoggingAxeJsonConverter.Read(json);
                case HarvestingSickleJsonConverter.DiscriminatorValue:
                    return HarvestingSickleJsonConverter.Read(json);
                case BaitJsonConverter.DiscriminatorValue:
                    return BaitJsonConverter.Read(json);
                case LureJsonConverter.DiscriminatorValue:
                    return LureJsonConverter.Read(json);
                case FishingRodJsonConverter.DiscriminatorValue:
                    return FishingRodJsonConverter.Read(json);
            }
        }

        var iconString = json.GetProperty("icon").GetString();
        return new GatheringTool
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Level = json.GetProperty("level").GetInt32(),
            Rarity = json.GetProperty("rarity").GetEnum<Rarity>(),
            VendorValue = json.GetProperty("vendor_value").GetInt32(),
            GameTypes =
                json.GetProperty("game_types").GetList(static (in JsonElement value) => value.GetEnum<GameType>()),
            Flags = ItemFlagsJsonConverter.Read(json.GetProperty("flags")),
            Restrictions = ItemRestrictionJsonConverter.Read(json.GetProperty("restrictions")),
            ChatLink = json.GetProperty("chat_link").GetStringRequired(),
#pragma warning disable CS0618 // Suppress obsolete warning
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = !string.IsNullOrEmpty(iconString) ? new Uri(iconString) : null
        };
    }

    public static void Write(Utf8JsonWriter writer, GatheringTool value)
    {
        switch (value)
        {
            case MiningPick miningPick:
                MiningPickJsonConverter.Write(writer, miningPick);
                break;
            case LoggingAxe loggingAxe:
                LoggingAxeJsonConverter.Write(writer, loggingAxe);
                break;
            case HarvestingSickle harvestingSickle:
                HarvestingSickleJsonConverter.Write(writer, harvestingSickle);
                break;
            case Bait bait:
                BaitJsonConverter.Write(writer, bait);
                break;
            case Lure lure:
                LureJsonConverter.Write(writer, lure);
                break;
            case FishingRod fishingRod:
                FishingRodJsonConverter.Write(writer, fishingRod);
                break;
            default:
                writer.WriteStartObject();
                writer.WriteString(ItemJsonConverter.DiscriminatorName, DiscriminatorValue);
                WriteCommonProperties(writer, value);
                writer.WriteEndObject();
                break;
        }
    }

    public static void WriteCommonProperties(Utf8JsonWriter writer, GatheringTool value)
    {
        ItemJsonConverter.WriteCommonProperties(writer, value);
    }
}
