using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Facts;

internal sealed class BuffJsonConverter : JsonConverter<Buff>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "buff";

    public override Buff Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Buff value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Buff Read(in JsonElement json)
    {
        if (json.TryGetProperty(DiscriminatorName, out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case PrefixedBuffJsonConverter.DiscriminatorValue:
                    return PrefixedBuffJsonConverter.Read(json);
                case DiscriminatorValue:
                    break;
                default:
                    ThrowHelper.ThrowInvalidDiscriminator(discriminator.GetString());
                    break;
            }
        }
        JsonElement textJson = json.GetProperty("text");
        JsonElement iconUrlJson = json.GetProperty("icon_url");
        JsonElement durationJson = json.GetProperty("duration");
        JsonElement statusJson = json.GetProperty("status");
        JsonElement descriptionJson = json.GetProperty("description");
        JsonElement applyCountJson = json.GetProperty("apply_count");
        return new Buff
        {
            Text = textJson.GetStringRequired(),
            IconUrl = iconUrlJson.ValueKind == JsonValueKind.Null ? null : new Uri(iconUrlJson.GetStringRequired(), UriKind.RelativeOrAbsolute),
            Duration = durationJson.ValueKind == JsonValueKind.Null ? null : TimeSpan.FromTicks(durationJson.GetInt64()),
            Status = statusJson.GetStringRequired(),
            Description = descriptionJson.GetStringRequired(),
            ApplyCount = applyCountJson.ValueKind == JsonValueKind.Null ? null : applyCountJson.GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, Buff value)
    {
        if (value is PrefixedBuff prefixedBuff)
        {
            PrefixedBuffJsonConverter.Write(writer, prefixedBuff);
        }
        else if (value.GetType() == typeof(Buff))
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
            if (value.Duration is { } presentDuration)
            {
                writer.WriteNumber("duration", presentDuration.Ticks);
            }
            else
            {
                writer.WriteNull("duration");
            }
            writer.WriteString("status", value.Status);
            writer.WriteString("description", value.Description);
            if (value.ApplyCount is { } presentApplyCount)
            {
                writer.WriteNumber("apply_count", presentApplyCount);
            }
            else
            {
                writer.WriteNull("apply_count");
            }
            writer.WriteEndObject();
        }
        else
        {
            ThrowHelper.ThrowInvalidOperationException("No JSON converter is registered for " + value.GetType() + ".");
        }
    }
}
