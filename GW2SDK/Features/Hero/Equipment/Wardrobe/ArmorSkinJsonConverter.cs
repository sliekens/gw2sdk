using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Items;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal sealed class ArmorSkinJsonConverter : JsonConverter<ArmorSkin>
{
    public const string DiscriminatorValue = "armor_skin";

    public const string DiscriminatorName = "$armor_skin_type";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(ArmorSkin).IsAssignableFrom(typeToConvert);
    }

    public override ArmorSkin Read(
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
        ArmorSkin value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static ArmorSkin Read(in JsonElement json)
    {
        if (!json.GetProperty(EquipmentSkinJsonConverter.DiscriminatorName)
            .ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(EquipmentSkinJsonConverter.DiscriminatorName).GetString()
            );
        }

        if (json.TryGetProperty(DiscriminatorName, out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case BootsSkinJsonConverter.DiscriminatorValue:
                    return BootsSkinJsonConverter.Read(json);
                case GlovesSkinJsonConverter.DiscriminatorValue:
                    return GlovesSkinJsonConverter.Read(json);
                case CoatSkinJsonConverter.DiscriminatorValue:
                    return CoatSkinJsonConverter.Read(json);
                case LeggingsSkinJsonConverter.DiscriminatorValue:
                    return LeggingsSkinJsonConverter.Read(json);
                case ShouldersSkinJsonConverter.DiscriminatorValue:
                    return ShouldersSkinJsonConverter.Read(json);
                case HelmSkinJsonConverter.DiscriminatorValue:
                    return HelmSkinJsonConverter.Read(json);
                case HelmAquaticSkinJsonConverter.DiscriminatorValue:
                    return HelmAquaticSkinJsonConverter.Read(json);
                default:
                    break;
            }
        }

        string iconString = json.GetProperty("icon").GetString() ?? "";
        return new ArmorSkin
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Flags = SkinFlagsJsonConverter.Read(json.GetProperty("flags")),
            Races = json.GetProperty("races").GetList(static (in JsonElement value) => value.GetEnum<RaceName>()),
            Rarity = json.GetProperty("rarity").GetEnum<Rarity>(),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = string.IsNullOrEmpty(iconString) ? null : new Uri(iconString),
            WeightClass = json.GetProperty("weight_class").GetEnum<WeightClass>(),
            DyeSlots = json.GetProperty("dye_slots").GetNullable(DyeSlotInfoJsonConverter.Read)
        };
    }

    public static void Write(Utf8JsonWriter writer, ArmorSkin value)
    {
        switch (value)
        {
            case BootsSkin boots:
                BootsSkinJsonConverter.Write(writer, boots);
                break;
            case GlovesSkin gloves:
                GlovesSkinJsonConverter.Write(writer, gloves);
                break;
            case CoatSkin coat:
                CoatSkinJsonConverter.Write(writer, coat);
                break;
            case LeggingsSkin leggings:
                LeggingsSkinJsonConverter.Write(writer, leggings);
                break;
            case ShouldersSkin shoulders:
                ShouldersSkinJsonConverter.Write(writer, shoulders);
                break;
            case HelmSkin helm:
                HelmSkinJsonConverter.Write(writer, helm);
                break;
            case HelmAquaticSkin helmAquatic:
                HelmAquaticSkinJsonConverter.Write(writer, helmAquatic);
                break;
            default:
                writer.WriteStartObject();
                writer.WriteString(
                    EquipmentSkinJsonConverter.DiscriminatorName,
                    DiscriminatorValue
                );
                writer.WriteString(DiscriminatorName, DiscriminatorValue);
                WriteCommonProperties(writer, value);
                writer.WriteEndObject();
                break;
        }
    }

    public static void WriteCommonProperties(Utf8JsonWriter writer, ArmorSkin value)
    {
        EquipmentSkinJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteString("weight_class", value.WeightClass.ToString());
        writer.WritePropertyName("dye_slots");
        if (value.DyeSlots is not null)
        {
            DyeSlotInfoJsonConverter.Write(writer, value.DyeSlots);
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
