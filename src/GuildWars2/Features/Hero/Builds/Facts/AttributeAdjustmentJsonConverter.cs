using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Facts;

internal sealed class AttributeAdjustmentJsonConverter : JsonConverter<AttributeAdjustment>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "attribute_adjustment";

    public override AttributeAdjustment Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, AttributeAdjustment value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static AttributeAdjustment Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement textJson = json.GetProperty("text");
        JsonElement iconUrlJson = json.GetProperty("icon_url");
        JsonElement valueJson = json.GetProperty("value");
        JsonElement targetJson = json.GetProperty("target");
        JsonElement hitCountJson = json.GetProperty("hit_count");
        return new AttributeAdjustment
        {
            Text = textJson.GetStringRequired(),
            IconUrl = iconUrlJson.ValueKind == JsonValueKind.Null ? null : new Uri(iconUrlJson.GetStringRequired(), UriKind.RelativeOrAbsolute),
            Value = valueJson.ValueKind == JsonValueKind.Null ? null : valueJson.GetInt32(),
            Target = targetJson.ValueKind == JsonValueKind.Null ? null : (GuildWars2.Extensible<GuildWars2.Hero.AttributeName>?)targetJson.GetEnum<GuildWars2.Hero.AttributeName>(),
            HitCount = hitCountJson.ValueKind == JsonValueKind.Null ? null : hitCountJson.GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, AttributeAdjustment value)
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
        if (value.Value is { } presentValue)
        {
            writer.WriteNumber("value", presentValue);
        }
        else
        {
            writer.WriteNull("value");
        }
        if (value.Target is { } presentTarget)
        {
            writer.WriteString("target", presentTarget.ToString());
        }
        else
        {
            writer.WriteNull("target");
        }
        if (value.HitCount is { } presentHitCount)
        {
            writer.WriteNumber("hit_count", presentHitCount);
        }
        else
        {
            writer.WriteNull("hit_count");
        }
        writer.WriteEndObject();
    }
}
