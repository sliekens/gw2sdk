﻿using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Items;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal sealed class FishingToolSkinJsonConverter : JsonConverter<FishingToolSkin>
{
    public const string DiscriminatorValue = "fishing_tool_skin";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(FishingToolSkin).IsAssignableFrom(typeToConvert);
    }

    public override FishingToolSkin Read(
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
        FishingToolSkin value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static FishingToolSkin Read(JsonElement json)
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

        return new FishingToolSkin
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Flags = SkinFlagsJsonConverter.Read(json.GetProperty("flags")),
            Races = json.GetProperty("races").GetList(static value => value.GetEnum<RaceName>()),
            Rarity = json.GetProperty("rarity").GetEnum<Rarity>(),
            IconHref = json.GetProperty("icon").GetString()
        };
    }

    public static void Write(Utf8JsonWriter writer, FishingToolSkin value)
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
