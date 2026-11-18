using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Facts;

internal sealed class ComboFieldJsonConverter : JsonConverter<ComboField>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "combo_field";

    public override ComboField Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, ComboField value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static ComboField Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement textJson = json.GetProperty("text");
        JsonElement iconUrlJson = json.GetProperty("icon_url");
        JsonElement fieldJson = json.GetProperty("field");
        return new ComboField
        {
            Text = textJson.GetStringRequired(),
            IconUrl = iconUrlJson.ValueKind == JsonValueKind.Null ? null : new Uri(iconUrlJson.GetStringRequired(), UriKind.RelativeOrAbsolute),
            Field = fieldJson.GetEnum<ComboFieldName>()
        };
    }

    public static void Write(Utf8JsonWriter writer, ComboField value)
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
        writer.WriteString("field", value.Field.ToString());
        writer.WriteEndObject();
    }
}
