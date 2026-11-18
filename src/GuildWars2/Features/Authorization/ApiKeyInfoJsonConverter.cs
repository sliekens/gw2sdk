using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Authorization;

internal sealed class ApiKeyInfoJsonConverter : JsonConverter<ApiKeyInfo>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "api_key_info";

    public override ApiKeyInfo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, ApiKeyInfo value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static ApiKeyInfo Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement idJson = json.GetProperty("id");
        JsonElement nameJson = json.GetProperty("name");
        JsonElement permissionsJson = json.GetProperty("permissions");
        return new ApiKeyInfo
        {
            Id = idJson.GetStringRequired(),
            Name = nameJson.GetStringRequired(),
            Permissions = permissionsJson.GetList(static (in value) => value.GetEnum<Permission>())
        };
    }

    public static void Write(Utf8JsonWriter writer, ApiKeyInfo value)
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
        writer.WriteEndObject();
    }
}
