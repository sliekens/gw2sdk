using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Facts;

internal sealed class NumberJsonConverter : JsonConverter<Number>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "number";

    public override Number Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Number value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Number Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement textJson = json.GetProperty("text");
        JsonElement iconUrlJson = json.GetProperty("icon_url");
        JsonElement valueJson = json.GetProperty("value");
        return new Number
        {
            Text = textJson.GetStringRequired(),
            IconUrl = iconUrlJson.ValueKind == JsonValueKind.Null ? null : new Uri(iconUrlJson.GetStringRequired(), UriKind.RelativeOrAbsolute),
            Value = valueJson.GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, Number value)
    {
        writer.WriteStartObject();
        writer.WriteString(DiscriminatorName, DiscriminatorValue);
        writer.WriteString("text", value.Text);
        if (value.IconUrl is { } presentIconUrl)
        {
            writer.WriteString("icon_url", presentIconUrl.OriginalString);
        }
        else
        {
            writer.WriteNull("icon_url");
        }
        writer.WriteNumber("value", value.Value);
        writer.WriteEndObject();
    }
}
