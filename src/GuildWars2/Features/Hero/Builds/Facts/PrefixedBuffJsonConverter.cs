using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Facts;

internal sealed class PrefixedBuffJsonConverter : JsonConverter<PrefixedBuff>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "prefixed_buff";

    public override PrefixedBuff Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, PrefixedBuff value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static PrefixedBuff Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement textJson = json.GetProperty("text");
        JsonElement iconUrlJson = json.GetProperty("icon_url");
        JsonElement durationJson = json.GetProperty("duration");
        JsonElement statusJson = json.GetProperty("status");
        JsonElement descriptionJson = json.GetProperty("description");
        JsonElement applyCountJson = json.GetProperty("apply_count");
        JsonElement prefixIconUrlJson = json.GetProperty("prefix_icon_url");
        JsonElement preconditionJson = json.GetProperty("precondition");
        return new PrefixedBuff
        {
            Text = textJson.GetStringRequired(),
            IconUrl = iconUrlJson.ValueKind == JsonValueKind.Null ? null : new Uri(iconUrlJson.GetStringRequired(), UriKind.RelativeOrAbsolute),
            Duration = durationJson.ValueKind == JsonValueKind.Null ? null : TimeSpan.FromTicks(durationJson.GetInt64()),
            Status = statusJson.GetStringRequired(),
            Description = descriptionJson.GetStringRequired(),
            ApplyCount = applyCountJson.ValueKind == JsonValueKind.Null ? null : applyCountJson.GetInt32(),
            PrefixIconUrl = new Uri(prefixIconUrlJson.GetStringRequired(), UriKind.RelativeOrAbsolute),
            Precondition = preconditionJson.GetStringRequired()
        };
    }

    public static void Write(Utf8JsonWriter writer, PrefixedBuff value)
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
        writer.WriteString("prefix_icon_url", value.PrefixIconUrl.OriginalString);
        writer.WriteString("precondition", value.Precondition);
        writer.WriteEndObject();
    }
}
