using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Items;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal sealed class BackItemSkinJsonConverter : JsonConverter<BackItemSkin>
{
    public const string DiscriminatorValue = "back_skin";
    public const string PreviousDiscriminatorValue = "backpack_skin";

    public override BackItemSkin Read(
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
        BackItemSkin value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static BackItemSkin Read(in JsonElement json)
    {
        if (!json.GetProperty(EquipmentSkinJsonConverter.DiscriminatorName).ValueEquals(DiscriminatorValue)
            && !json.GetProperty(EquipmentSkinJsonConverter.DiscriminatorName).ValueEquals(PreviousDiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(EquipmentSkinJsonConverter.DiscriminatorName).GetString()
            );
        }

        string iconString = json.GetProperty("icon").GetString() ?? "";
        return new BackItemSkin
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

    public static void Write(Utf8JsonWriter writer, BackItemSkin value)
    {
        writer.WriteStartObject();
        writer.WriteString(EquipmentSkinJsonConverter.DiscriminatorName, DiscriminatorValue);
        EquipmentSkinJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteEndObject();
    }
}
