using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal sealed class InfluenceActivityJsonConverter : JsonConverter<InfluenceActivity>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "influence_activity";

    public override InfluenceActivity Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, InfluenceActivity value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static InfluenceActivity Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement idJson = json.GetProperty("id");
        JsonElement timeJson = json.GetProperty("time");
        JsonElement activityJson = json.GetProperty("activity");
        JsonElement totalParticipantsJson = json.GetProperty("total_participants");
        JsonElement participantsJson = json.GetProperty("participants");
        return new InfluenceActivity
        {
            Id = idJson.GetInt32(),
            Time = timeJson.GetDateTimeOffset(),
            Activity = activityJson.GetEnum<InfluenceActivityKind>(),
            TotalParticipants = totalParticipantsJson.GetInt32(),
            Participants = participantsJson.GetList(static (in value) => value.GetStringRequired())
        };
    }

    public static void Write(Utf8JsonWriter writer, InfluenceActivity value)
    {
        writer.WriteStartObject();
        writer.WriteString(DiscriminatorName, DiscriminatorValue);
        writer.WriteNumber("id", value.Id);
        writer.WriteString("time", value.Time);
        writer.WriteString("activity", value.Activity.ToString());
        writer.WriteNumber("total_participants", value.TotalParticipants);
        writer.WriteStartArray("participants");
        foreach (string item in value.Participants)
        {
            writer.WriteStringValue(item);
        }
        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
