using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Collections;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class RuneJsonConverter : JsonConverter<Rune>
{
    public const string DiscriminatorValue = "rune";

    public override Rune? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Rune value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Rune Read(in JsonElement json)
    {
        if (!json.GetProperty(ItemJsonConverter.DiscriminatorName)
            .ValueEquals(UpgradeComponentJsonConverter.DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(ItemJsonConverter.DiscriminatorName).GetString()
            );
        }

        if (!json.GetProperty(UpgradeComponentJsonConverter.DiscriminatorName)
            .ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(UpgradeComponentJsonConverter.DiscriminatorName).GetString()
            );
        }

        var iconString = json.GetProperty("icon").GetString();
        return new Rune
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
            Bonuses = json.GetProperty("bonuses")
                .GetNullable(static (in JsonElement value) => value.GetList(static (in JsonElement entry) => entry.GetStringRequired())),
            UpgradesInto =
                json.TryGetProperty("upgrades_into", out JsonElement found)
                    ? found.GetList(InfusionSlotUpgradePathJsonConverter.Read)
                    : new ValueList<InfusionSlotUpgradePath>()
        };
    }

    public static void Write(Utf8JsonWriter writer, Rune value)
    {
        writer.WriteStartObject();
        writer.WriteString(
            ItemJsonConverter.DiscriminatorName,
            UpgradeComponentJsonConverter.DiscriminatorValue
        );
        writer.WriteString(UpgradeComponentJsonConverter.DiscriminatorName, DiscriminatorValue);
        UpgradeComponentJsonConverter.WriteCommonProperties(writer, value);

        if (value.Bonuses is not null)
        {
            writer.WriteStartArray("bonuses");
            foreach (var bonus in value.Bonuses)
            {
                writer.WriteStringValue(bonus);
            }

            writer.WriteEndArray();
        }
        else
        {
            writer.WriteNull("bonuses");
        }

        writer.WriteEndObject();
    }
}
