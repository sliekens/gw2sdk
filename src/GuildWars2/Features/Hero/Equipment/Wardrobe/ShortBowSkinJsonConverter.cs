using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Items;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal sealed class ShortBowSkinJsonConverter : JsonConverter<ShortBowSkin>
{
    public const string DiscriminatorValue = "short_bow_skin";
    public const string PreviousDiscriminatorValue = "shortbow_skin";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(ShortBowSkin).IsAssignableFrom(typeToConvert);
    }

    public override ShortBowSkin Read(
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
        ShortBowSkin value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static ShortBowSkin Read(in JsonElement json)
    {
        if (!json.GetProperty(EquipmentSkinJsonConverter.DiscriminatorName)
            .ValueEquals(WeaponSkinJsonConverter.DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(EquipmentSkinJsonConverter.DiscriminatorName).GetString()
            );
        }

        if (!json.GetProperty(WeaponSkinJsonConverter.DiscriminatorName).ValueEquals(DiscriminatorValue)
            && !json.GetProperty(WeaponSkinJsonConverter.DiscriminatorName).ValueEquals(PreviousDiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(WeaponSkinJsonConverter.DiscriminatorName).GetString()
            );
        }

        string iconString = json.GetProperty("icon").GetString() ?? "";
        return new ShortBowSkin
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Flags = SkinFlagsJsonConverter.Read(json.GetProperty("flags")),
            Races = json.GetProperty("races").GetList(static (in value) => value.GetEnum<RaceName>()),
            Rarity = json.GetProperty("rarity").GetEnum<Rarity>(),
            IconUrl = string.IsNullOrEmpty(iconString) ? null : new Uri(iconString),
            DamageType = json.GetProperty("damage_type").GetEnum<DamageType>()
        };
    }

    public static void Write(Utf8JsonWriter writer, ShortBowSkin value)
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
