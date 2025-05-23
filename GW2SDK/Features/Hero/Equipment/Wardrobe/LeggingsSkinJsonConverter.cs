﻿using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Items;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal sealed class LeggingsSkinJsonConverter : JsonConverter<LeggingsSkin>
{
    public const string DiscriminatorValue = "leggings_skin";

    public override LeggingsSkin Read(
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
        LeggingsSkin value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static LeggingsSkin Read(JsonElement json)
    {
        if (!json.GetProperty(EquipmentSkinJsonConverter.DiscriminatorName)
            .ValueEquals(ArmorSkinJsonConverter.DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(EquipmentSkinJsonConverter.DiscriminatorName).GetString()
            );
        }

        if (!json.GetProperty(ArmorSkinJsonConverter.DiscriminatorName)
            .ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(ArmorSkinJsonConverter.DiscriminatorName).GetString()
            );
        }

        var iconString = json.GetProperty("icon").GetString() ?? "";
        return new LeggingsSkin
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Flags = SkinFlagsJsonConverter.Read(json.GetProperty("flags")),
            Races = json.GetProperty("races").GetList(static value => value.GetEnum<RaceName>()),
            Rarity = json.GetProperty("rarity").GetEnum<Rarity>(),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = string.IsNullOrEmpty(iconString) ? null : new Uri(iconString),
            WeightClass = json.GetProperty("weight_class").GetEnum<WeightClass>(),
            DyeSlots = json.GetProperty("dye_slots").GetNullable(DyeSlotInfoJsonConverter.Read)
        };
    }

    public static void Write(Utf8JsonWriter writer, LeggingsSkin value)
    {
        writer.WriteStartObject();
        writer.WriteString(
            EquipmentSkinJsonConverter.DiscriminatorName,
            ArmorSkinJsonConverter.DiscriminatorValue
        );
        writer.WriteString(ArmorSkinJsonConverter.DiscriminatorName, DiscriminatorValue);
        ArmorSkinJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteEndObject();
    }
}
