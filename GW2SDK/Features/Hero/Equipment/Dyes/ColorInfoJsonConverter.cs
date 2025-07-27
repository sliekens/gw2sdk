using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Dyes;

internal sealed class ColorInfoJsonConverter : JsonConverter<ColorInfo>
{
    public override ColorInfo Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var document = JsonDocument.ParseValue(ref reader);
        return Read(document.RootElement);
    }

    public static ColorInfo Read(in JsonElement json)
    {
        return new ColorInfo
        {
            Brightness = json.GetProperty("brightness").GetInt32(),
            Contrast = json.GetProperty("contrast").GetDouble(),
            Hue = json.GetProperty("hue").GetInt32(),
            Saturation = json.GetProperty("saturation").GetDouble(),
            Lightness = json.GetProperty("lightness").GetDouble(),
            Rgb = Color.FromArgb(json.GetProperty("rgb").GetInt32())
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        ColorInfo value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static void Write(Utf8JsonWriter writer, ColorInfo value)
    {
        writer.WriteStartObject();
        writer.WriteNumber("brightness", value.Brightness);
        writer.WriteNumber("contrast", value.Contrast);
        writer.WriteNumber("hue", value.Hue);
        writer.WriteNumber("saturation", value.Saturation);
        writer.WriteNumber("lightness", value.Lightness);
        writer.WriteNumber("rgb", value.Rgb.ToArgb());
        writer.WriteEndObject();
    }
}
