using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Items;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal sealed class TorchSkinJsonConverter : JsonConverter<TorchSkin>
{
    public const string DiscriminatorValue = "torch_skin";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(TorchSkin).IsAssignableFrom(typeToConvert);
    }

    public override TorchSkin Read(
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
        TorchSkin value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static TorchSkin Read(in JsonElement json)
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

        string iconString = json.GetProperty("icon").GetString() ?? "";
        return new TorchSkin
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

    public static void Write(Utf8JsonWriter writer, TorchSkin value)
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
