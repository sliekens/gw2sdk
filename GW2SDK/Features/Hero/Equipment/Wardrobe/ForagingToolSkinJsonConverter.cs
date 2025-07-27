using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Items;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal sealed class ForagingToolSkinJsonConverter : JsonConverter<ForagingToolSkin>
{
    public const string DiscriminatorValue = "foraging_tool_skin";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(ForagingToolSkin).IsAssignableFrom(typeToConvert);
    }

    public override ForagingToolSkin Read(
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
        ForagingToolSkin value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static ForagingToolSkin Read(in JsonElement json)
    {
        if (!json.GetProperty(EquipmentSkinJsonConverter.DiscriminatorName)
            .ValueEquals(GatheringToolSkinJsonConverter.DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(EquipmentSkinJsonConverter.DiscriminatorName).GetString()
            );
        }

        if (!json.GetProperty(GatheringToolSkinJsonConverter.DiscriminatorName)
            .ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(GatheringToolSkinJsonConverter.DiscriminatorName).GetString()
            );
        }

        var iconString = json.GetProperty("icon").GetString() ?? "";
        return new ForagingToolSkin
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
            IconUrl = string.IsNullOrEmpty(iconString) ? null : new Uri(iconString)
        };
    }

    public static void Write(Utf8JsonWriter writer, ForagingToolSkin value)
    {
        writer.WriteStartObject();
        writer.WriteString(
            EquipmentSkinJsonConverter.DiscriminatorName,
            GatheringToolSkinJsonConverter.DiscriminatorValue
        );
        writer.WriteString(GatheringToolSkinJsonConverter.DiscriminatorName, DiscriminatorValue);
        GatheringToolSkinJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteEndObject();
    }
}
