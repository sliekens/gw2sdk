using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Authorization;

internal sealed class SubtokenInfoJsonConverter : JsonConverter<SubtokenInfo>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "subtoken_info";

    public override SubtokenInfo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, SubtokenInfo value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static SubtokenInfo Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement idJson = json.GetProperty("id");
        JsonElement nameJson = json.GetProperty("name");
        JsonElement permissionsJson = json.GetProperty("permissions");
        JsonElement expiresAtJson = json.GetProperty("expires_at");
        JsonElement issuedAtJson = json.GetProperty("issued_at");
        JsonElement urlsJson = json.GetProperty("urls");
        return new SubtokenInfo
        {
            Id = idJson.GetStringRequired(),
            Name = nameJson.GetStringRequired(),
            Permissions = permissionsJson.GetList(static (in value) => value.GetEnum<Permission>()),
            ExpiresAt = expiresAtJson.GetDateTimeOffset(),
            IssuedAt = issuedAtJson.GetDateTimeOffset(),
            Urls = urlsJson.ValueKind == JsonValueKind.Null ? null : urlsJson.GetList(static (in value) => new Uri(value.GetStringRequired(), UriKind.RelativeOrAbsolute))
        };
    }

    public static void Write(Utf8JsonWriter writer, SubtokenInfo value)
    {
        writer.WriteStartObject();
        writer.WriteString(DiscriminatorName, DiscriminatorValue);
        writer.WriteString("id", value.Id);
        writer.WriteString("name", value.Name);
        writer.WriteStartArray("permissions");
        foreach (GuildWars2.Extensible<Permission> item in value.Permissions)
        {
            writer.WriteStringValue(item.ToString());
        }
        writer.WriteEndArray();
        writer.WriteString("expires_at", value.ExpiresAt);
        writer.WriteString("issued_at", value.IssuedAt);
        if (value.Urls is { } presentUrls)
        {
            writer.WriteStartArray("urls");
            foreach (Uri item in presentUrls)
            {
                writer.WriteStringValue(item.OriginalString);
            }
            writer.WriteEndArray();
        }
        else
        {
            writer.WriteNull("urls");
        }
        writer.WriteEndObject();
    }
}
