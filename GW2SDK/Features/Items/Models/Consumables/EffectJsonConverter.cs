using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class EffectJsonConverter : JsonConverter<Effect>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(Effect).IsAssignableFrom(typeToConvert);
    }

    public override Effect Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Effect value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Effect Read(in JsonElement json)
    {
        string iconString = "";
        if (json.TryGetProperty("icon", out var iconElement))
        {
            iconString = iconElement.GetString() ?? "";
        }
        else if (json.TryGetProperty("icon_href", out iconElement))
        {
            iconString = iconElement.GetString() ?? "";
        }

#pragma warning disable CS0618 // Suppress obsolete warning
        return new Effect
        {
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Duration = TimeSpan.Parse(json.GetProperty("duration").GetStringRequired()),
            ApplyCount = json.GetProperty("apply_count").GetInt32(),
#pragma warning disable CS0618 // Suppress obsolete warning
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = !string.IsNullOrEmpty(iconString) ? new Uri(iconString) : null
        };
#pragma warning restore CS0618
    }

    public static void Write(Utf8JsonWriter writer, Effect value)
    {
        writer.WriteStartObject();
        writer.WriteString("name", value.Name);
        writer.WriteString("description", value.Description);
        writer.WriteString("duration", value.Duration.ToString());
        writer.WriteNumber("apply_count", value.ApplyCount);
        writer.WriteString("icon", value.IconUrl?.ToString());
        writer.WriteEndObject();
    }
}
