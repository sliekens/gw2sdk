using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Items;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal sealed class HelmAquaticSkinJsonConverter : JsonConverter<HelmAquaticSkin>
{
    public const string DiscriminatorValue = "helm_aquatic_skin";

    public override HelmAquaticSkin Read(
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
        HelmAquaticSkin value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static HelmAquaticSkin Read(in JsonElement json)
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

        string iconString = json.GetProperty("icon").GetString() ?? "";
        return new HelmAquaticSkin
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Flags = SkinFlagsJsonConverter.Read(json.GetProperty("flags")),
            Races = json.GetProperty("races").GetList(static (in value) => value.GetEnum<RaceName>()),
            Rarity = json.GetProperty("rarity").GetEnum<Rarity>(),
            IconUrl = string.IsNullOrEmpty(iconString) ? null : new Uri(iconString),
            WeightClass = json.GetProperty("weight_class").GetEnum<WeightClass>(),
            DyeSlots = json.GetProperty("dye_slots").GetNullable(DyeSlotInfoJsonConverter.Read)
        };
    }

    public static void Write(Utf8JsonWriter writer, HelmAquaticSkin value)
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
