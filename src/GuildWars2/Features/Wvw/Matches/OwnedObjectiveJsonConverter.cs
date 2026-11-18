using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

internal sealed class OwnedObjectiveJsonConverter : JsonConverter<OwnedObjective>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "owned_objective";

    public override OwnedObjective Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, OwnedObjective value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static OwnedObjective Read(in JsonElement json)
    {
        if (json.TryGetProperty(DiscriminatorName, out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case OwnedCampJsonConverter.DiscriminatorValue:
                    return OwnedCampJsonConverter.Read(json);
                case OwnedCastleJsonConverter.DiscriminatorValue:
                    return OwnedCastleJsonConverter.Read(json);
                case OwnedKeepJsonConverter.DiscriminatorValue:
                    return OwnedKeepJsonConverter.Read(json);
                case OwnedMercenaryJsonConverter.DiscriminatorValue:
                    return OwnedMercenaryJsonConverter.Read(json);
                case OwnedRuinsJsonConverter.DiscriminatorValue:
                    return OwnedRuinsJsonConverter.Read(json);
                case OwnedSpawnJsonConverter.DiscriminatorValue:
                    return OwnedSpawnJsonConverter.Read(json);
                case OwnedTowerJsonConverter.DiscriminatorValue:
                    return OwnedTowerJsonConverter.Read(json);
                case DiscriminatorValue:
                    break;
                default:
                    ThrowHelper.ThrowInvalidDiscriminator(discriminator.GetString());
                    break;
            }
        }
        JsonElement idJson = json.GetProperty("id");
        JsonElement ownerJson = json.GetProperty("owner");
        JsonElement lastFlippedJson = json.GetProperty("last_flipped");
        JsonElement pointsTickJson = json.GetProperty("points_tick");
        JsonElement pointsCaptureJson = json.GetProperty("points_capture");
        return new OwnedObjective
        {
            Id = idJson.GetStringRequired(),
            Owner = ownerJson.GetEnum<GuildWars2.Wvw.TeamColor>(),
            LastFlipped = lastFlippedJson.GetDateTimeOffset(),
            PointsTick = pointsTickJson.GetInt32(),
            PointsCapture = pointsCaptureJson.GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, OwnedObjective value)
    {
        if (value is OwnedCamp ownedCamp)
        {
            OwnedCampJsonConverter.Write(writer, ownedCamp);
        }
        else if (value is OwnedCastle ownedCastle)
        {
            OwnedCastleJsonConverter.Write(writer, ownedCastle);
        }
        else if (value is OwnedKeep ownedKeep)
        {
            OwnedKeepJsonConverter.Write(writer, ownedKeep);
        }
        else if (value is OwnedMercenary ownedMercenary)
        {
            OwnedMercenaryJsonConverter.Write(writer, ownedMercenary);
        }
        else if (value is OwnedRuins ownedRuins)
        {
            OwnedRuinsJsonConverter.Write(writer, ownedRuins);
        }
        else if (value is OwnedSpawn ownedSpawn)
        {
            OwnedSpawnJsonConverter.Write(writer, ownedSpawn);
        }
        else if (value is OwnedTower ownedTower)
        {
            OwnedTowerJsonConverter.Write(writer, ownedTower);
        }
        else if (value.GetType() == typeof(OwnedObjective))
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
        else
        {
            ThrowHelper.ThrowInvalidOperationException("No JSON converter is registered for " + value.GetType() + ".");
        }
    }
}
