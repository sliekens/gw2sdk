using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal class SkinFlagsJsonConverter : JsonConverter<SkinFlags>
{
    public override SkinFlags? Read(
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
        SkinFlags value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static SkinFlags Read(in JsonElement json)
    {
        return new()
        {
            HideIfLocked = json.GetProperty("hide_if_locked").GetBoolean(),
            NoCost = json.GetProperty("no_cost").GetBoolean(),
            OverrideRarity = json.GetProperty("override_rarity").GetBoolean(),
            ShowInWardrobe = json.GetProperty("show_in_wardrobe").GetBoolean(),
            Other = json.GetProperty("other").GetList(static (in JsonElement value) => value.GetStringRequired())
        };
    }

    public static void Write(Utf8JsonWriter writer, SkinFlags value)
    {
        writer.WriteStartObject();
        writer.WriteBoolean("hide_if_locked", value.HideIfLocked);
        writer.WriteBoolean("no_cost", value.NoCost);
        writer.WriteBoolean("override_rarity", value.OverrideRarity);
        writer.WriteBoolean("show_in_wardrobe", value.ShowInWardrobe);
        writer.WriteStartArray("other");
        foreach (var other in value.Other)
        {
            writer.WriteStringValue(other);
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
