using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

internal sealed class OwnedTowerJsonConverter : JsonConverter<OwnedTower>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "owned_tower";

    public override OwnedTower Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, OwnedTower value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static OwnedTower Read(in JsonElement json)
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
        JsonElement claimedByJson = json.GetProperty("claimed_by");
        JsonElement claimedAtJson = json.GetProperty("claimed_at");
        JsonElement yaksDeliveredJson = json.GetProperty("yaks_delivered");
        JsonElement guildUpgradesJson = json.GetProperty("guild_upgrades");
        return new OwnedTower
        {
            Id = idJson.GetStringRequired(),
            Owner = ownerJson.GetEnum<GuildWars2.Wvw.TeamColor>(),
            LastFlipped = lastFlippedJson.GetDateTimeOffset(),
            PointsTick = pointsTickJson.GetInt32(),
            PointsCapture = pointsCaptureJson.GetInt32(),
            ClaimedBy = claimedByJson.GetStringRequired(),
            ClaimedAt = claimedAtJson.ValueKind == JsonValueKind.Null ? null : claimedAtJson.GetDateTimeOffset(),
            YaksDelivered = yaksDeliveredJson.GetInt32(),
            GuildUpgrades = guildUpgradesJson.GetList(static (in value) => value.GetInt32())
        };
    }

    public static void Write(Utf8JsonWriter writer, OwnedTower value)
    {
        writer.WriteStartObject();
        writer.WriteString(DiscriminatorName, DiscriminatorValue);
        writer.WriteString("id", value.Id);
        writer.WriteString("owner", value.Owner.ToString());
        writer.WriteString("last_flipped", value.LastFlipped);
        writer.WriteNumber("points_tick", value.PointsTick);
        writer.WriteNumber("points_capture", value.PointsCapture);
        writer.WriteString("claimed_by", value.ClaimedBy);
        if (value.ClaimedAt is { } presentClaimedAt)
        {
            writer.WriteString("claimed_at", presentClaimedAt);
        }
        else
        {
            writer.WriteNull("claimed_at");
        }
        writer.WriteNumber("yaks_delivered", value.YaksDelivered);
        writer.WriteStartArray("guild_upgrades");
        foreach (int item in value.GuildUpgrades)
        {
            writer.WriteNumberValue(item);
        }
        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
