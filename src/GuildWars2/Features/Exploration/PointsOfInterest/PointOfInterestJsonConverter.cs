using System.Drawing;

using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Exploration.PointsOfInterest;

internal sealed class PointOfInterestJsonConverter : JsonConverter<PointOfInterest>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "point_of_interest";

    public override PointOfInterest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, PointOfInterest value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static PointOfInterest Read(in JsonElement json)
    {
        if (json.TryGetProperty(DiscriminatorName, out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case LandmarkJsonConverter.DiscriminatorValue:
                    return LandmarkJsonConverter.Read(json);
                case RequiresUnlockPointOfInterestJsonConverter.DiscriminatorValue:
                    return RequiresUnlockPointOfInterestJsonConverter.Read(json);
                case VistaJsonConverter.DiscriminatorValue:
                    return VistaJsonConverter.Read(json);
                case WaypointJsonConverter.DiscriminatorValue:
                    return WaypointJsonConverter.Read(json);
                case DiscriminatorValue:
                    break;
                default:
                    ThrowHelper.ThrowInvalidDiscriminator(discriminator.GetString());
                    break;
            }
        }
        JsonElement nameJson = json.GetProperty("name");
        JsonElement idJson = json.GetProperty("id");
        JsonElement floorJson = json.GetProperty("floor");
        JsonElement coordinatesJson = json.GetProperty("coordinates");
        JsonElement chatLinkJson = json.GetProperty("chat_link");
        return new PointOfInterest
        {
            Name = nameJson.GetStringRequired(),
            Id = idJson.GetInt32(),
            Floor = floorJson.GetInt32(),
            Coordinates = new PointF(coordinatesJson[0].GetSingle(), coordinatesJson[1].GetSingle()),
            ChatLink = chatLinkJson.GetStringRequired()
        };
    }

    public static void Write(Utf8JsonWriter writer, PointOfInterest value)
    {
        if (value is Landmark landmark)
        {
            LandmarkJsonConverter.Write(writer, landmark);
        }
        else if (value is RequiresUnlockPointOfInterest requiresUnlockPointOfInterest)
        {
            RequiresUnlockPointOfInterestJsonConverter.Write(writer, requiresUnlockPointOfInterest);
        }
        else if (value is Vista vista)
        {
            VistaJsonConverter.Write(writer, vista);
        }
        else if (value is Waypoint waypoint)
        {
            WaypointJsonConverter.Write(writer, waypoint);
        }
        else if (value.GetType() == typeof(PointOfInterest))
        {
            writer.WriteStartObject();
            writer.WriteString(DiscriminatorName, DiscriminatorValue);
            writer.WriteString("name", value.Name);
            writer.WriteNumber("id", value.Id);
            writer.WriteNumber("floor", value.Floor);
            writer.WriteStartArray("coordinates");
            writer.WriteNumberValue(value.Coordinates.X);
            writer.WriteNumberValue(value.Coordinates.Y);
            writer.WriteEndArray();
            writer.WriteString("chat_link", value.ChatLink);
            writer.WriteEndObject();
        }
        else
        {
            ThrowHelper.ThrowInvalidOperationException("No JSON converter is registered for " + value.GetType() + ".");
        }
    }
}
