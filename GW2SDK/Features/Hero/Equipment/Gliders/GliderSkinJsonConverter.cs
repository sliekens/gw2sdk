using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Gliders;

internal sealed class GliderSkinJsonConverter : JsonConverter<GliderSkin>
{
    public override GliderSkin? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var document = JsonDocument.ParseValue(ref reader);
        return Read(document.RootElement);
    }

    public static GliderSkin? Read(in JsonElement json)
    {
        var iconString = json.GetProperty("icon").GetStringRequired();
        return new GliderSkin
        {
            Id = json.GetProperty("id").GetInt32(),
            UnlockItemIds = json.GetProperty("unlock_item_ids").GetList(static (in JsonElement item) => item.GetInt32()),
            Order = json.GetProperty("order").GetInt32(),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            DefaultDyeColorIds =
                json.GetProperty("default_dye_color_ids").GetList(static (in JsonElement item) => item.GetInt32())
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        GliderSkin value,
        JsonSerializerOptions options
    )
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WriteStartArray("unlock_item_ids");
        foreach (var element in value.UnlockItemIds)
        {
            writer.WriteNumberValue(element);
        }

        writer.WriteEndArray();
        writer.WriteNumber("order", value.Order);
        writer.WriteString("icon", value.IconUrl.ToString());
        writer.WriteString("name", value.Name);
        writer.WriteString("description", value.Description);
        writer.WriteStartArray("default_dye_color_ids");
        foreach (var element in value.DefaultDyeColorIds)
        {
            writer.WriteNumberValue(element);
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
