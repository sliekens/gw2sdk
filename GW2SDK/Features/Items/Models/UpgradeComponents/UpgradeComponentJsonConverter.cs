﻿using System.Text.Json;
using System.Text.Json.Serialization;
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

    public override UpgradeComponent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, UpgradeComponent value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static UpgradeComponent Read(JsonElement json)
    {
        if (!json.GetProperty(ItemJsonConverter.DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(ItemJsonConverter.DiscriminatorName).GetString());
        }

        if (json.TryGetProperty(DiscriminatorName, out var discriminator))
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

        return new UpgradeComponent
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Level = json.GetProperty("level").GetInt32(),
            Rarity = json.GetProperty("rarity").GetEnum<Rarity>(),
            VendorValue = json.GetProperty("vendor_value").GetInt32(),
            GameTypes = json.GetProperty("game_types").GetList(static value => value.GetEnum<GameType>()),
            Flags = ItemFlagsJsonConverter.Read(json.GetProperty("flags")),
            Restrictions = ItemRestrictionJsonConverter.Read(json.GetProperty("restrictions")),
            ChatLink = json.GetProperty("chat_link").GetStringRequired(),
            IconHref = json.GetProperty("icon").GetString(),
            UpgradeComponentFlags = UpgradeComponentFlagsJsonConverter.Read(json.GetProperty("upgrade_component_flags")),
            InfusionUpgradeFlags = InfusionSlotFlagsJsonConverter.Read(json.GetProperty("infusion_upgrade_flags")),
            AttributeAdjustment = json.GetProperty("attribute_adjustment").GetDouble(),
            AttributeCombinationId = json.GetProperty("attribute_combination_id").GetNullableInt32(),
            Attributes = json.GetProperty("attributes").GetMap(
                static name => new Extensible<AttributeName>(name),
                static value => value.GetInt32()
            ),
            Buff = json.GetProperty("buff").GetNullable(BuffJsonConverter.Read),
            SuffixName = json.GetProperty("suffix").GetStringRequired()
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

        writer.WriteString("suffix", value.SuffixName);
    }
}