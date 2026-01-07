using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class BackItemJsonConverter : JsonConverter<BackItem>
{
    public const string DiscriminatorValue = "back";

    public override BackItem? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, BackItem value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static BackItem Read(in JsonElement json)
    {
        if (json.TryGetProperty(ItemJsonConverter.DiscriminatorName, out JsonElement discriminator) && !discriminator.ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(discriminator.GetString());
        }

        string? iconString = json.GetProperty("icon").GetString();
        return new BackItem
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Level = json.GetProperty("level").GetInt32(),
            Rarity = json.GetProperty("rarity").GetEnum<Rarity>(),
            VendorValue = json.GetProperty("vendor_value").GetInt32(),
            GameTypes =
                json.GetProperty("game_types").GetList(static (in value) => value.GetEnum<GameType>()),
            Flags = ItemFlagsJsonConverter.Read(json.GetProperty("flags")),
            Restrictions = ItemRestrictionJsonConverter.Read(json.GetProperty("restrictions")),
            ChatLink = json.GetProperty("chat_link").GetStringRequired(),
            IconUrl = !string.IsNullOrEmpty(iconString) ? new Uri(iconString) : null,
            DefaultSkinId = json.GetProperty("default_skin_id").GetInt32(),
            InfusionSlots = json.GetProperty("infusion_slots")
                .GetList(InfusionSlotJsonConverter.Read),
            AttributeAdjustment = json.GetProperty("attribute_adjustment").GetDouble(),
            AttributeCombinationId =
                json.GetProperty("attribute_combination_id").GetNullableInt32(),
            Attributes =
                json.GetProperty("attributes")
                    .GetMap(
                        static name => new Extensible<AttributeName>(name),
                        static (in value) => value.GetInt32()
                    ),
            Buff = json.GetProperty("buff").GetNullable(BuffJsonConverter.Read),
            SuffixItemId = json.GetProperty("suffix_item_id").GetNullableInt32(),
            StatChoices = json.GetProperty("stat_choices").GetList(static (in value) => value.GetInt32()),
            UpgradesInto =
                json.GetProperty("upgrades_into")
                    .GetList(InfusionSlotUpgradePathJsonConverter.Read),
            UpgradesFrom = json.GetProperty("upgrades_from")
                .GetList(InfusionSlotUpgradeSourceJsonConverter.Read)
        };
    }

    public static void Write(Utf8JsonWriter writer, BackItem value)
    {
        writer.WriteStartObject();
        writer.WriteString(ItemJsonConverter.DiscriminatorName, DiscriminatorValue);
        ItemJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteNumber("default_skin_id", value.DefaultSkinId);

        writer.WriteStartArray("infusion_slots");
        foreach (InfusionSlot? slot in value.InfusionSlots)
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

        writer.WriteStartObject("attributes");
        foreach (KeyValuePair<Extensible<AttributeName>, int> attribute in value.Attributes)
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

        writer.WriteStartArray("stat_choices");
        foreach (int statChoice in value.StatChoices)
        {
            writer.WriteNumberValue(statChoice);
        }

        writer.WriteEndArray();

        writer.WriteStartArray("upgrades_into");
        foreach (InfusionSlotUpgradePath? upgrade in value.UpgradesInto)
        {
            InfusionSlotUpgradePathJsonConverter.Write(writer, upgrade);
        }

        writer.WriteEndArray();

        writer.WriteStartArray("upgrades_from");
        foreach (InfusionSlotUpgradeSource? source in value.UpgradesFrom)
        {
            InfusionSlotUpgradeSourceJsonConverter.Write(writer, source);
        }

        writer.WriteEndArray();

        writer.WriteEndObject();
    }
}
