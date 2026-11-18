using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal sealed class InviteDeclinedJsonConverter : JsonConverter<InviteDeclined>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "invite_declined";

    public override InviteDeclined Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, InviteDeclined value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static InviteDeclined Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement idJson = json.GetProperty("id");
        JsonElement timeJson = json.GetProperty("time");
        JsonElement userJson = json.GetProperty("user");
        JsonElement declinedByJson = json.GetProperty("declined_by");
        return new InviteDeclined
        {
            Id = idJson.GetInt32(),
            Time = timeJson.GetDateTimeOffset(),
            User = userJson.GetStringRequired(),
            DeclinedBy = declinedByJson.GetStringRequired()
        };
    }

    public static void Write(Utf8JsonWriter writer, InviteDeclined value)
    {
        writer.WriteStartObject();
        writer.WriteString(DiscriminatorName, DiscriminatorValue);
        writer.WriteNumber("id", value.Id);
        writer.WriteString("time", value.Time);
        writer.WriteString("user", value.User);
        writer.WriteString("declined_by", value.DeclinedBy);
        writer.WriteEndObject();
    }
}
