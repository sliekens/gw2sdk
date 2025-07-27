using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Dyes;

internal sealed class DyeColorJsonConverter : JsonConverter<DyeColor>
{
    public override DyeColor? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var document = JsonDocument.ParseValue(ref reader);
        var json = document.RootElement;
        return new DyeColor
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            BaseRgb = Color.FromArgb(json.GetProperty("base_rgb").GetInt32()),
            Cloth = ColorInfoJsonConverter.Read(json.GetProperty("cloth")),
            Leather = ColorInfoJsonConverter.Read(json.GetProperty("leather")),
            Metal = ColorInfoJsonConverter.Read(json.GetProperty("metal")),
            Fur = json.GetProperty("fur").GetNullable(ColorInfoJsonConverter.Read),
            ItemId = json.GetProperty("item_id").GetNullableInt32(),
            Hue = json.GetProperty("hue").GetEnum<Hue>(),
            Material = json.GetProperty("material").GetEnum<Material>(),
            Set = json.GetProperty("set").GetEnum<ColorSet>()
        };
    }

    public override void Write(Utf8JsonWriter writer, DyeColor value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WriteString("name", value.Name);
        writer.WriteNumber("base_rgb", value.BaseRgb.ToArgb());
        writer.WritePropertyName("cloth");
        ColorInfoJsonConverter.Write(writer, value.Cloth);
        writer.WritePropertyName("leather");
        ColorInfoJsonConverter.Write(writer, value.Leather);
        writer.WritePropertyName("metal");
        ColorInfoJsonConverter.Write(writer, value.Metal);
        writer.WritePropertyName("fur");
        if (value.Fur is not null)
        {
            ColorInfoJsonConverter.Write(writer, value.Fur);
        }
        else
        {
            writer.WriteNullValue();
        }

        if (value.ItemId.HasValue)
        {
            writer.WriteNumber("item_id", value.ItemId.Value);
        }
        else
        {
            writer.WriteNull("item_id");
        }

        writer.WriteString("hue", value.Hue.ToString());
        writer.WriteString("material", value.Material.ToString());
        writer.WriteString("set", value.Set.ToString());
        writer.WriteEndObject();
    }
}
