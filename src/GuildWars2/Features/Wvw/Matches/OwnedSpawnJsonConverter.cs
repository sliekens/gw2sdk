using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

internal sealed class OwnedSpawnJsonConverter : JsonConverter<OwnedSpawn>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "owned_spawn";

    public override OwnedSpawn Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, OwnedSpawn value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static OwnedSpawn Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement idJson = json.GetProperty("id");
        JsonElement ownerJson = json.GetProperty("owner");
        JsonElement lastFlippedJson = json.GetProperty("last_flipped");
        JsonElement pointsTickJson = json.GetProperty("points_tick");
        JsonElement pointsCaptureJson = json.GetProperty("points_capture");
        return new OwnedSpawn
        {
            Id = idJson.GetStringRequired(),
            Owner = ownerJson.GetEnum<GuildWars2.Wvw.TeamColor>(),
            LastFlipped = lastFlippedJson.GetDateTimeOffset(),
            PointsTick = pointsTickJson.GetInt32(),
            PointsCapture = pointsCaptureJson.GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, OwnedSpawn value)
    {
        writer.WriteStartObject();
        writer.WriteString(DiscriminatorName, DiscriminatorValue);
        writer.WriteString("id", value.Id);
        writer.WriteString("owner", value.Owner.ToString());
        writer.WriteString("last_flipped", value.LastFlipped);
        writer.WriteNumber("points_tick", value.PointsTick);
        writer.WriteNumber("points_capture", value.PointsCapture);
        writer.WriteEndObject();
    }
}
