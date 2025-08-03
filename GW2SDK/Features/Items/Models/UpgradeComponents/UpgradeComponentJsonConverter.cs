using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Collections;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class UpgradeComponentJsonConverter : JsonConverter<UpgradeComponent>
{
    public const string DiscriminatorValue = "upgrade_component";

    public const string DiscriminatorName = "$upgrade_component_type";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(UpgradeComponent).IsAssignableFrom(typeToConvert);
    }

    public override UpgradeComponent Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(
        Utf8JsonWriter writer,
        UpgradeComponent value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static UpgradeComponent Read(in JsonElement json)
    {
        if (!json.GetProperty(ItemJsonConverter.DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(ItemJsonConverter.DiscriminatorName).GetString()
            );
        }

        if (json.TryGetProperty(DiscriminatorName, out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case GemJsonConverter.DiscriminatorValue:
                    return GemJsonConverter.Read(json);
                case RuneJsonConverter.DiscriminatorValue:
                    return RuneJsonConverter.Read(json);
                case SigilJsonConverter.DiscriminatorValue:
                    return SigilJsonConverter.Read(json);
            }
        }

        string? iconString = json.GetProperty("icon").GetString();
        return new UpgradeComponent
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
            UpgradeComponentFlags =
                UpgradeComponentFlagsJsonConverter.Read(
                    json.GetProperty("upgrade_component_flags")
                ),
            InfusionUpgradeFlags =
                InfusionSlotFlagsJsonConverter.Read(json.GetProperty("infusion_upgrade_flags")),
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
            SuffixName = json.GetProperty("suffix").GetStringRequired(),
            UpgradesInto =
                json.TryGetProperty("upgrades_into", out JsonElement found)
                    ? found.GetList(InfusionSlotUpgradePathJsonConverter.Read)
                    : []
        };
    }

    public static void Write(Utf8JsonWriter writer, UpgradeComponent value)
    {
        switch (value)
        {
            case Gem gem:
                GemJsonConverter.Write(writer, gem);
                break;
            case Rune rune:
                RuneJsonConverter.Write(writer, rune);
                break;
            case Sigil sigil:
                SigilJsonConverter.Write(writer, sigil);
                break;
            default:
                writer.WriteStartObject();
                writer.WriteString(ItemJsonConverter.DiscriminatorName, DiscriminatorValue);
                WriteCommonProperties(writer, value);
                writer.WriteEndObject();
                break;
        }
    }

    public static void WriteCommonProperties(Utf8JsonWriter writer, UpgradeComponent value)
    {
        ItemJsonConverter.WriteCommonProperties(writer, value);

        writer.WritePropertyName("upgrade_component_flags");
        UpgradeComponentFlagsJsonConverter.Write(writer, value.UpgradeComponentFlags);

        writer.WritePropertyName("infusion_upgrade_flags");
        InfusionSlotFlagsJsonConverter.Write(writer, value.InfusionUpgradeFlags);

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

        writer.WriteString("suffix", value.SuffixName);

        writer.WriteStartArray("upgrades_into");
        foreach (InfusionSlotUpgradePath upgrade in value.UpgradesInto)
        {
            InfusionSlotUpgradePathJsonConverter.Write(writer, upgrade);
        }

        writer.WriteEndArray();
    }
}
