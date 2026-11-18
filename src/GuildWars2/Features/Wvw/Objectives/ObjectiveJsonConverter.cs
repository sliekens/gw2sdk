using System.Drawing;
using System.Numerics;

using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Wvw.Objectives;

internal sealed class ObjectiveJsonConverter : JsonConverter<Objective>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "objective";

    public override Objective Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Objective value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Objective Read(in JsonElement json)
    {
        if (json.TryGetProperty(DiscriminatorName, out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case CampJsonConverter.DiscriminatorValue:
                    return CampJsonConverter.Read(json);
                case CastleJsonConverter.DiscriminatorValue:
                    return CastleJsonConverter.Read(json);
                case KeepJsonConverter.DiscriminatorValue:
                    return KeepJsonConverter.Read(json);
                case MercenaryJsonConverter.DiscriminatorValue:
                    return MercenaryJsonConverter.Read(json);
                case ResourceJsonConverter.DiscriminatorValue:
                    return ResourceJsonConverter.Read(json);
                case RuinsJsonConverter.DiscriminatorValue:
                    return RuinsJsonConverter.Read(json);
                case SpawnJsonConverter.DiscriminatorValue:
                    return SpawnJsonConverter.Read(json);
                case TowerJsonConverter.DiscriminatorValue:
                    return TowerJsonConverter.Read(json);
                case DiscriminatorValue:
                    break;
                default:
                    ThrowHelper.ThrowInvalidDiscriminator(discriminator.GetString());
                    break;
            }
        }
        JsonElement idJson = json.GetProperty("id");
        JsonElement nameJson = json.GetProperty("name");
        JsonElement sectorIdJson = json.GetProperty("sector_id");
        JsonElement mapKindJson = json.GetProperty("map_kind");
        JsonElement mapIdJson = json.GetProperty("map_id");
        JsonElement upgradeIdJson = json.GetProperty("upgrade_id");
        JsonElement coordinatesJson = json.GetProperty("coordinates");
        JsonElement labelCoordinatesJson = json.GetProperty("label_coordinates");
        JsonElement markerIconUrlJson = json.GetProperty("marker_icon_url");
        JsonElement chatLinkJson = json.GetProperty("chat_link");
        return new Objective
        {
            Id = idJson.GetStringRequired(),
            Name = nameJson.GetStringRequired(),
            SectorId = sectorIdJson.GetInt32(),
            MapKind = mapKindJson.GetEnum<GuildWars2.Exploration.Maps.MapKind>(),
            MapId = mapIdJson.GetInt32(),
            UpgradeId = upgradeIdJson.ValueKind == JsonValueKind.Null ? null : upgradeIdJson.GetInt32(),
            Coordinates = coordinatesJson.ValueKind == JsonValueKind.Null ? null : new Vector3(coordinatesJson[0].GetSingle(), coordinatesJson[1].GetSingle(), coordinatesJson[2].GetSingle()),
            LabelCoordinates = labelCoordinatesJson.ValueKind == JsonValueKind.Null ? null : new PointF(labelCoordinatesJson[0].GetSingle(), labelCoordinatesJson[1].GetSingle()),
            MarkerIconUrl = markerIconUrlJson.ValueKind == JsonValueKind.Null ? null : new Uri(markerIconUrlJson.GetStringRequired(), UriKind.RelativeOrAbsolute),
            ChatLink = chatLinkJson.GetStringRequired()
        };
    }

    public static void Write(Utf8JsonWriter writer, Objective value)
    {
        if (value is Camp camp)
        {
            CampJsonConverter.Write(writer, camp);
        }
        else if (value is Castle castle)
        {
            CastleJsonConverter.Write(writer, castle);
        }
        else if (value is Keep keep)
        {
            KeepJsonConverter.Write(writer, keep);
        }
        else if (value is Mercenary mercenary)
        {
            MercenaryJsonConverter.Write(writer, mercenary);
        }
        else if (value is Resource resource)
        {
            ResourceJsonConverter.Write(writer, resource);
        }
        else if (value is Ruins ruins)
        {
            RuinsJsonConverter.Write(writer, ruins);
        }
        else if (value is Spawn spawn)
        {
            SpawnJsonConverter.Write(writer, spawn);
        }
        else if (value is Tower tower)
        {
            TowerJsonConverter.Write(writer, tower);
        }
        else if (value.GetType() == typeof(Objective))
        {
            writer.WriteStartObject();
            writer.WriteString(DiscriminatorName, DiscriminatorValue);
            writer.WriteString("id", value.Id);
            writer.WriteString("name", value.Name);
            writer.WriteNumber("sector_id", value.SectorId);
            writer.WriteString("map_kind", value.MapKind.ToString());
            writer.WriteNumber("map_id", value.MapId);
            if (value.UpgradeId is { } presentUpgradeId)
            {
                writer.WriteNumber("upgrade_id", presentUpgradeId);
            }
            else
            {
                writer.WriteNull("upgrade_id");
            }
            if (value.Coordinates is { } presentCoordinates)
            {
                writer.WriteStartArray("coordinates");
                writer.WriteNumberValue(presentCoordinates.X);
                writer.WriteNumberValue(presentCoordinates.Y);
                writer.WriteNumberValue(presentCoordinates.Z);
                writer.WriteEndArray();
            }
            else
            {
                writer.WriteNull("coordinates");
            }
            if (value.LabelCoordinates is { } presentLabelCoordinates)
            {
                writer.WriteStartArray("label_coordinates");
                writer.WriteNumberValue(presentLabelCoordinates.X);
                writer.WriteNumberValue(presentLabelCoordinates.Y);
                writer.WriteEndArray();
            }
            else
            {
                writer.WriteNull("label_coordinates");
            }
            if (value.MarkerIconUrl is { } presentMarkerIconUrl)
            {
                writer.WriteString("marker_icon_url", presentMarkerIconUrl.OriginalString);
            }
            else
            {
                writer.WriteNull("marker_icon_url");
            }
            writer.WriteString("chat_link", value.ChatLink);
            writer.WriteEndObject();
        }
        else
        {
            ThrowHelper.ThrowInvalidOperationException("No JSON converter is registered for " + value.GetType() + ".");
        }
    }
}
