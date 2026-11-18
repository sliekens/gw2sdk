using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal sealed class GuildMissionJsonConverter : JsonConverter<GuildMission>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "guild_mission";

    public override GuildMission Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, GuildMission value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static GuildMission Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement idJson = json.GetProperty("id");
        JsonElement timeJson = json.GetProperty("time");
        JsonElement userJson = json.GetProperty("user");
        JsonElement stateJson = json.GetProperty("state");
        JsonElement influenceJson = json.GetProperty("influence");
        return new GuildMission
        {
            Id = idJson.GetInt32(),
            Time = timeJson.GetDateTimeOffset(),
            User = userJson.GetStringRequired(),
            State = stateJson.GetEnum<GuildMissionState>(),
            Influence = influenceJson.GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, GuildMission value)
    {
        writer.WriteStartObject();
        writer.WriteString(DiscriminatorName, DiscriminatorValue);
        writer.WriteNumber("id", value.Id);
        writer.WriteString("time", value.Time);
        writer.WriteString("user", value.User);
        writer.WriteString("state", value.State.ToString());
        writer.WriteNumber("influence", value.Influence);
        writer.WriteEndObject();
    }
}
