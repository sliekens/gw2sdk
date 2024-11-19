﻿
using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class AccessoryJsonConverter : JsonConverter<Accessory>
{
    public const string DiscriminatorValue = "accessory";

    public override Accessory? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Accessory value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Accessory Read(JsonElement json)
    {
        if (!json.GetProperty(ItemJsonConverter.DiscriminatorName).ValueEquals(TrinketJsonConverter.DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(ItemJsonConverter.DiscriminatorName).GetString());
        }

        if (!json.GetProperty(TrinketJsonConverter.DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(TrinketJsonConverter.DiscriminatorName).GetString());
        }

        return new Accessory
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
            InfusionSlots = json.GetProperty("infusion_slots").GetList(InfusionSlotJsonConverter.Read),
            AttributeAdjustment = json.GetProperty("attribute_adjustment").GetDouble(),
            AttributeCombinationId = json.GetProperty("attribute_combination_id").GetNullableInt32(),
            Attributes = json.GetProperty("attributes").GetMap(
                static name => new Extensible<AttributeName>(name),
                static value => value.GetInt32()
            ),
            Buff = json.GetProperty("buff").GetNullable(BuffJsonConverter.Read),
            SuffixItemId = json.GetProperty("suffix_item_id").GetNullableInt32(),
            StatChoices = json.GetProperty("stat_choices").GetList(value => value.GetInt32())
        };
    }

    public static void Write(Utf8JsonWriter writer, Accessory value)
    {
        writer.WriteStartObject();
        writer.WriteString(ItemJsonConverter.DiscriminatorName, TrinketJsonConverter.DiscriminatorValue);
        writer.WriteString(TrinketJsonConverter.DiscriminatorName, DiscriminatorValue);
        TrinketJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteEndObject();
    }
}
