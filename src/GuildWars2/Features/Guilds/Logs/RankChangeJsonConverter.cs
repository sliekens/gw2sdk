using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal sealed class RankChangeJsonConverter : JsonConverter<RankChange>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "rank_change";

    public override RankChange Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, RankChange value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static RankChange Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement idJson = json.GetProperty("id");
        JsonElement timeJson = json.GetProperty("time");
        JsonElement userJson = json.GetProperty("user");
        JsonElement oldRankJson = json.GetProperty("old_rank");
        JsonElement newRankJson = json.GetProperty("new_rank");
        JsonElement changedByJson = json.GetProperty("changed_by");
        return new RankChange
        {
            Id = idJson.GetInt32(),
            Time = timeJson.GetDateTimeOffset(),
            User = userJson.GetStringRequired(),
            OldRank = oldRankJson.GetStringRequired(),
            NewRank = newRankJson.GetStringRequired(),
            ChangedBy = changedByJson.GetStringRequired()
        };
    }

    public static void Write(Utf8JsonWriter writer, RankChange value)
    {
        writer.WriteStartObject();
        writer.WriteString(DiscriminatorName, DiscriminatorValue);
        writer.WriteNumber("id", value.Id);
        writer.WriteString("time", value.Time);
        writer.WriteString("user", value.User);
        writer.WriteString("old_rank", value.OldRank);
        writer.WriteString("new_rank", value.NewRank);
        writer.WriteString("changed_by", value.ChangedBy);
        writer.WriteEndObject();
    }
}
