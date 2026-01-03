using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Items;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal sealed class EquipmentSkinJsonConverter : JsonConverter<EquipmentSkin>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "skin";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(EquipmentSkin).IsAssignableFrom(typeToConvert);
    }

    public override EquipmentSkin Read(
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
        EquipmentSkin value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static EquipmentSkin Read(in JsonElement json)
    {
        if (json.TryGetProperty(DiscriminatorName, out JsonElement discriminator))
        {
            switch (discriminator.ToString())
            {
                case ArmorSkinJsonConverter.DiscriminatorValue:
                    return ArmorSkinJsonConverter.Read(json);
                case BackpackSkinJsonConverter.DiscriminatorValue:
                    return BackpackSkinJsonConverter.Read(json);
                case GatheringToolSkinJsonConverter.DiscriminatorValue:
                    return GatheringToolSkinJsonConverter.Read(json);
                case WeaponSkinJsonConverter.DiscriminatorValue:
                    return WeaponSkinJsonConverter.Read(json);
                default:
                    break;
            }
        }

        string? iconString = json.GetProperty("icon").GetString();
        return new EquipmentSkin
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Flags = SkinFlagsJsonConverter.Read(json.GetProperty("flags")),
            Races = json.GetProperty("races").GetList(static (in value) => value.GetEnum<RaceName>()),
            Rarity = json.GetProperty("rarity").GetEnum<Rarity>(),
            IconUrl = string.IsNullOrEmpty(iconString) ? null : new Uri(iconString)
        };
    }

    public static void Write(Utf8JsonWriter writer, EquipmentSkin value)
    {
        switch (value)
        {
            case ArmorSkin armorSkin:
                ArmorSkinJsonConverter.Write(writer, armorSkin);
                break;
            case BackpackSkin backpackSkin:
                BackpackSkinJsonConverter.Write(writer, backpackSkin);
                break;
            case GatheringToolSkin gatheringToolSkin:
                GatheringToolSkinJsonConverter.Write(writer, gatheringToolSkin);
                break;
            case WeaponSkin weaponSkin:
                WeaponSkinJsonConverter.Write(writer, weaponSkin);
                break;
            default:
                writer.WriteStartObject();
                writer.WriteString(DiscriminatorName, DiscriminatorValue);
                WriteCommonProperties(writer, value);
                writer.WriteEndObject();
                break;
        }
    }

    public static void WriteCommonProperties(Utf8JsonWriter writer, EquipmentSkin value)
    {
        writer.WriteNumber("id", value.Id);
        writer.WriteString("name", value.Name);
        writer.WriteString("description", value.Description);
        writer.WritePropertyName("flags");
        SkinFlagsJsonConverter.Write(writer, value.Flags);
        writer.WriteStartArray("races");
        foreach (Extensible<RaceName> race in value.Races)
        {
            writer.WriteStringValue(race.ToString());
        }

        writer.WriteEndArray();
        writer.WriteString("rarity", value.Rarity.ToString());
        writer.WriteString("icon", value.IconUrl?.ToString());
    }
}
