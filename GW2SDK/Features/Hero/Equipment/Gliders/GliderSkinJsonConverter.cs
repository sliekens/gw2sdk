using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Gliders;

internal sealed class GliderSkinJsonConverter : JsonConverter<GliderSkin>
{
    public override GliderSkin? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var document = JsonDocument.ParseValue(ref reader);
        return Read(document.RootElement);
    }

    public static GliderSkin? Read(JsonElement json)
    {
        return new GliderSkin
        {
            Id = json.GetProperty("id").GetInt32(),
            UnlockItemIds = json.GetProperty("unlock_item_ids").GetList(item => item.GetInt32()),
            Order = json.GetProperty("order").GetInt32(),
            IconHref = json.GetProperty("icon").GetStringRequired(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            DefaultDyeColorIds = json.GetProperty("default_dye_color_ids").GetList(item => item.GetInt32())
        };
    }

    public override void Write(Utf8JsonWriter writer, GliderSkin value, JsonSerializerOptions options)
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
        writer.WriteString("icon", value.IconHref);
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
