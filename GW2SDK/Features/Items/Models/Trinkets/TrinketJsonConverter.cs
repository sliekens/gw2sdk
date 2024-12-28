using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class TrinketJsonConverter : JsonConverter<Trinket>
{
    public const string DiscriminatorValue = "trinket";

    public const string DiscriminatorName = "$trinket_type";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(Trinket).IsAssignableFrom(typeToConvert);
    }

    public override Trinket Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Trinket value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Trinket Read(JsonElement json)
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
                case AmuletJsonConverter.DiscriminatorValue:
                    return AmuletJsonConverter.Read(json);
                case RingJsonConverter.DiscriminatorValue:
                    return RingJsonConverter.Read(json);
                case AccessoryJsonConverter.DiscriminatorValue:
                    return AccessoryJsonConverter.Read(json);
            }
        }

        return new Trinket
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Level = json.GetProperty("level").GetInt32(),
            Rarity = json.GetProperty("rarity").GetEnum<Rarity>(),
            VendorValue = json.GetProperty("vendor_value").GetInt32(),
            GameTypes =
                json.GetProperty("game_types").GetList(static value => value.GetEnum<GameType>()),
            Flags = ItemFlagsJsonConverter.Read(json.GetProperty("flags")),
            Restrictions = ItemRestrictionJsonConverter.Read(json.GetProperty("restrictions")),
            ChatLink = json.GetProperty("chat_link").GetStringRequired(),
            IconHref = json.GetProperty("icon").GetString(),
            InfusionSlots = json.GetProperty("infusion_slots")
                .GetList(InfusionSlotJsonConverter.Read),
            AttributeAdjustment = json.GetProperty("attribute_adjustment").GetDouble(),
            AttributeCombinationId =
                json.GetProperty("attribute_combination_id").GetNullableInt32(),
            Attributes =
                json.GetProperty("attributes")
                    .GetMap(
                        static name => new Extensible<AttributeName>(name),
                        static value => value.GetInt32()
                    ),
            Buff = json.GetProperty("buff").GetNullable(BuffJsonConverter.Read),
            SuffixItemId = json.GetProperty("suffix_item_id").GetNullableInt32(),
            StatChoices = json.GetProperty("stat_choices").GetList(value => value.GetInt32())
        };
    }

    public static void Write(Utf8JsonWriter writer, Trinket value)
    {
        switch (value)
        {
            case Amulet amulet:
                AmuletJsonConverter.Write(writer, amulet);
                break;
            case Ring ring:
                RingJsonConverter.Write(writer, ring);
                break;
            case Accessory accessory:
                AccessoryJsonConverter.Write(writer, accessory);
                break;
            default:
                writer.WriteStartObject();
                writer.WriteString(ItemJsonConverter.DiscriminatorName, DiscriminatorValue);
                WriteCommonProperties(writer, value);
                writer.WriteEndObject();
                break;
        }
    }

    public static void WriteCommonProperties(Utf8JsonWriter writer, Trinket value)
    {
        ItemJsonConverter.WriteCommonProperties(writer, value);

        writer.WritePropertyName("infusion_slots");
        writer.WriteStartArray();
        foreach (var slot in value.InfusionSlots)
        {
            InfusionSlotJsonConverter.Write(writer, slot);
        }

        writer.WriteEndArray();

        writer.WriteNumber("attribute_adjustment", value.AttributeAdjustment);

        if (value.AttributeCombinationId.HasValue)
        {
            writer.WriteNumber("attribute_combination_id", value.AttributeCombinationId.Value);
        }
        else
        {
            writer.WriteNull("attribute_combination_id");
        }

        writer.WritePropertyName("attributes");
        writer.WriteStartObject();
        foreach (var attribute in value.Attributes)
        {
            writer.WriteNumber(attribute.Key.ToString(), attribute.Value);
        }

        writer.WriteEndObject();

        if (value.Buff is not null)
        {
            writer.WritePropertyName("buff");
            BuffJsonConverter.Write(writer, value.Buff);
        }
        else
        {
            writer.WriteNull("buff");
        }

        if (value.SuffixItemId.HasValue)
        {
            writer.WriteNumber("suffix_item_id", value.SuffixItemId.Value);
        }
        else
        {
            writer.WriteNull("suffix_item_id");
        }

        writer.WritePropertyName("stat_choices");
        writer.WriteStartArray();
        foreach (var statChoice in value.StatChoices)
        {
            writer.WriteNumberValue(statChoice);
        }

        writer.WriteEndArray();
    }
}
