using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Authorization;

internal sealed class TokenInfoJsonConverter : JsonConverter<TokenInfo>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "token_info";

    public override TokenInfo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, TokenInfo value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static TokenInfo Read(in JsonElement json)
    {
        if (json.TryGetProperty(DiscriminatorName, out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case ApiKeyInfoJsonConverter.DiscriminatorValue:
                    return ApiKeyInfoJsonConverter.Read(json);
                case SubtokenInfoJsonConverter.DiscriminatorValue:
                    return SubtokenInfoJsonConverter.Read(json);
                case DiscriminatorValue:
                    break;
                default:
                    ThrowHelper.ThrowInvalidDiscriminator(discriminator.GetString());
                    break;
            }
        }
        JsonElement idJson = json.GetProperty("id");
        JsonElement nameJson = json.GetProperty("name");
        JsonElement permissionsJson = json.GetProperty("permissions");
        return new TokenInfo
        {
            Id = idJson.GetStringRequired(),
            Name = nameJson.GetStringRequired(),
            Permissions = permissionsJson.GetList(static (in value) => value.GetEnum<Permission>())
        };
    }

    public static void Write(Utf8JsonWriter writer, TokenInfo value)
    {
        if (value is ApiKeyInfo apiKeyInfo)
        {
            ApiKeyInfoJsonConverter.Write(writer, apiKeyInfo);
        }
        else if (value is SubtokenInfo subtokenInfo)
        {
            SubtokenInfoJsonConverter.Write(writer, subtokenInfo);
        }
        else if (value.GetType() == typeof(TokenInfo))
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
        else
        {
            ThrowHelper.ThrowInvalidOperationException("No JSON converter is registered for " + value.GetType() + ".");
        }
    }
}
