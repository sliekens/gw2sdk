using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Items;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal sealed class BackpackSkinJsonConverter : JsonConverter<BackpackSkin>
{
    public const string DiscriminatorValue = "backpack_skin";

    public override BackpackSkin Read(
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
        BackpackSkin value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static BackpackSkin Read(in JsonElement json)
    {
        if (!json.GetProperty(EquipmentSkinJsonConverter.DiscriminatorName)
            .ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(EquipmentSkinJsonConverter.DiscriminatorName).GetString()
            );
        }

        string iconString = json.GetProperty("icon").GetString() ?? "";
        return new BackpackSkin
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Flags = SkinFlagsJsonConverter.Read(json.GetProperty("flags")),
            Races = json.GetProperty("races").GetList(static (in value) => value.GetEnum<RaceName>()),
            Rarity = json.GetProperty("rarity").GetEnum<Rarity>(),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = string.IsNullOrEmpty(iconString) ? null : new Uri(iconString)
        };
    }

    public static void Write(Utf8JsonWriter writer, BackpackSkin value)
    {
        writer.WriteStartObject();
        writer.WriteString(EquipmentSkinJsonConverter.DiscriminatorName, DiscriminatorValue);
        EquipmentSkinJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteEndObject();
    }
}
