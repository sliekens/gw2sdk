﻿using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Items;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal sealed class ShortbowSkinJsonConverter : JsonConverter<ShortbowSkin>
{
    public const string DiscriminatorValue = "shortbow_skin";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(ShortbowSkin).IsAssignableFrom(typeToConvert);
    }

    public override ShortbowSkin Read(
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
        ShortbowSkin value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static ShortbowSkin Read(JsonElement json)
    {
        if (!json.GetProperty(EquipmentSkinJsonConverter.DiscriminatorName)
            .ValueEquals(WeaponSkinJsonConverter.DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(EquipmentSkinJsonConverter.DiscriminatorName).GetString()
            );
        }

        if (!json.GetProperty(WeaponSkinJsonConverter.DiscriminatorName)
            .ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(WeaponSkinJsonConverter.DiscriminatorName).GetString()
            );
        }

        return new ShortbowSkin
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Flags = SkinFlagsJsonConverter.Read(json.GetProperty("flags")),
            Races = json.GetProperty("races").GetList(static value => value.GetEnum<RaceName>()),
            Rarity = json.GetProperty("rarity").GetEnum<Rarity>(),
            IconHref = json.GetProperty("icon").GetString(),
            DamageType = json.GetProperty("damage_type").GetEnum<DamageType>()
        };
    }

    public static void Write(Utf8JsonWriter writer, ShortbowSkin value)
    {
        writer.WriteStartObject();
        writer.WriteString(
            EquipmentSkinJsonConverter.DiscriminatorName,
            WeaponSkinJsonConverter.DiscriminatorValue
        );
        writer.WriteString(WeaponSkinJsonConverter.DiscriminatorName, DiscriminatorValue);
        WeaponSkinJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteEndObject();
    }
}
