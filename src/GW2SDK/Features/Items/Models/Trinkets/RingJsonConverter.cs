using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class RingJsonConverter : JsonConverter<Ring>
{
    public const string DiscriminatorValue = "ring";

    public override Ring Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Ring value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Ring Read(in JsonElement json)
    {
        if (!json.GetProperty(ItemJsonConverter.DiscriminatorName)
            .ValueEquals(TrinketJsonConverter.DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(ItemJsonConverter.DiscriminatorName).GetString()
            );
        }

        if (!json.GetProperty(TrinketJsonConverter.DiscriminatorName)
            .ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(TrinketJsonConverter.DiscriminatorName).GetString()
            );
        }

        string? iconString = json.GetProperty("icon").GetString();
        return new Ring
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
            IconUrl = !string.IsNullOrEmpty(iconString) ? new Uri(iconString) : null,
            InfusionSlots = json.GetProperty("infusion_slots")
                .GetList(InfusionSlotJsonConverter.Read),
            AttributeAdjustment = json.GetProperty("attribute_adjustment").GetDouble(),
            AttributeCombinationId =
                json.GetProperty("attribute_combination_id").GetNullableInt32(),
            Attributes =
                json.GetProperty("attributes")
                    .GetMap(
                        static name => new Extensible<AttributeName>(name),
                        static (in JsonElement value) => value.GetInt32()
                    ),
            Buff = json.GetProperty("buff").GetNullable(BuffJsonConverter.Read),
            SuffixItemId = json.GetProperty("suffix_item_id").GetNullableInt32(),
            StatChoices = json.GetProperty("stat_choices").GetList(static (in JsonElement value) => value.GetInt32()),
            UpgradesInto =
                json.GetProperty("upgrades_into")
                    .GetList(InfusionSlotUpgradePathJsonConverter.Read),
            UpgradesFrom = json.GetProperty("upgrades_from")
                .GetList(InfusionSlotUpgradeSourceJsonConverter.Read)
        };
    }

    public static void Write(Utf8JsonWriter writer, Ring value)
    {
        writer.WriteStartObject();
        writer.WriteString(
            ItemJsonConverter.DiscriminatorName,
            TrinketJsonConverter.DiscriminatorValue
        );
        writer.WriteString(TrinketJsonConverter.DiscriminatorName, DiscriminatorValue);
        TrinketJsonConverter.WriteCommonProperties(writer, value);

        writer.WriteStartArray("upgrades_into");
        foreach (InfusionSlotUpgradePath upgrade in value.UpgradesInto)
        {
            InfusionSlotUpgradePathJsonConverter.Write(writer, upgrade);
        }

        writer.WriteEndArray();

        writer.WriteStartArray("upgrades_from");
        foreach (InfusionSlotUpgradeSource source in value.UpgradesFrom)
        {
            InfusionSlotUpgradeSourceJsonConverter.Write(writer, source);
        }

        writer.WriteEndArray();

        writer.WriteEndObject();
    }
}
