﻿using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Items;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal sealed class CoatSkinJsonConverter : JsonConverter<CoatSkin>
{
    public const string DiscriminatorValue = "coat_skin";

    public override CoatSkin Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, CoatSkin value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static CoatSkin Read(JsonElement json)
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

        return new CoatSkin
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Flags = SkinFlagsJsonConverter.Read(json.GetProperty("flags")),
            Races = json.GetProperty("races").GetList(static value => value.GetEnum<RaceName>()),
            Rarity = json.GetProperty("rarity").GetEnum<Rarity>(),
            IconHref = json.GetProperty("icon").GetString(),
            WeightClass = json.GetProperty("weight_class").GetEnum<WeightClass>(),
            DyeSlots = json.GetProperty("dye_slots").GetNullable(DyeSlotInfoJsonConverter.Read)
        };
    }

    public static void Write(Utf8JsonWriter writer, CoatSkin value)
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
