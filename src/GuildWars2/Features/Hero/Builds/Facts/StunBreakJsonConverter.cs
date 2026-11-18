using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Facts;

internal sealed class StunBreakJsonConverter : JsonConverter<StunBreak>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "stun_break";

    public override StunBreak Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, StunBreak value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static StunBreak Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement textJson = json.GetProperty("text");
        JsonElement iconUrlJson = json.GetProperty("icon_url");
        return new StunBreak
        {
            Text = textJson.GetStringRequired(),
            IconUrl = iconUrlJson.ValueKind == JsonValueKind.Null ? null : new Uri(iconUrlJson.GetStringRequired(), UriKind.RelativeOrAbsolute)
        };
    }

    public static void Write(Utf8JsonWriter writer, StunBreak value)
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
        writer.WriteEndObject();
    }
}
