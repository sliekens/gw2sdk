using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal sealed class MemberKickedJsonConverter : JsonConverter<MemberKicked>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "member_kicked";

    public override MemberKicked Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, MemberKicked value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static MemberKicked Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement idJson = json.GetProperty("id");
        JsonElement timeJson = json.GetProperty("time");
        JsonElement userJson = json.GetProperty("user");
        JsonElement kickedByJson = json.GetProperty("kicked_by");
        return new MemberKicked
        {
            Id = idJson.GetInt32(),
            Time = timeJson.GetDateTimeOffset(),
            User = userJson.GetStringRequired(),
            KickedBy = kickedByJson.GetStringRequired()
        };
    }

    public static void Write(Utf8JsonWriter writer, MemberKicked value)
    {
        writer.WriteStartObject();
        writer.WriteString(DiscriminatorName, DiscriminatorValue);
        writer.WriteNumber("id", value.Id);
        writer.WriteString("time", value.Time);
        writer.WriteString("user", value.User);
        writer.WriteString("kicked_by", value.KickedBy);
        writer.WriteEndObject();
    }
}
