using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Facts;

internal sealed class ComboFinisherJsonConverter : JsonConverter<ComboFinisher>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "combo_finisher";

    public override ComboFinisher Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, ComboFinisher value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static ComboFinisher Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement textJson = json.GetProperty("text");
        JsonElement iconUrlJson = json.GetProperty("icon_url");
        JsonElement percentJson = json.GetProperty("percent");
        JsonElement finisherNameJson = json.GetProperty("finisher_name");
        return new ComboFinisher
        {
            Text = textJson.GetStringRequired(),
            IconUrl = iconUrlJson.ValueKind == JsonValueKind.Null ? null : new Uri(iconUrlJson.GetStringRequired(), UriKind.RelativeOrAbsolute),
            Percent = percentJson.GetInt32(),
            FinisherName = finisherNameJson.GetEnum<ComboFinisherName>()
        };
    }

    public static void Write(Utf8JsonWriter writer, ComboFinisher value)
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
        writer.WriteNumber("percent", value.Percent);
        writer.WriteString("finisher_name", value.FinisherName.ToString());
        writer.WriteEndObject();
    }
}
