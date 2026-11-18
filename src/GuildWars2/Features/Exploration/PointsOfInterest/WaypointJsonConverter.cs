using System.Drawing;

using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Exploration.PointsOfInterest;

internal sealed class WaypointJsonConverter : JsonConverter<Waypoint>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "waypoint";

    public override Waypoint Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Waypoint value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Waypoint Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement nameJson = json.GetProperty("name");
        JsonElement idJson = json.GetProperty("id");
        JsonElement floorJson = json.GetProperty("floor");
        JsonElement coordinatesJson = json.GetProperty("coordinates");
        JsonElement chatLinkJson = json.GetProperty("chat_link");
        return new Waypoint
        {
            Name = nameJson.GetStringRequired(),
            Id = idJson.GetInt32(),
            Floor = floorJson.GetInt32(),
            Coordinates = new PointF(coordinatesJson[0].GetSingle(), coordinatesJson[1].GetSingle()),
            ChatLink = chatLinkJson.GetStringRequired()
        };
    }

    public static void Write(Utf8JsonWriter writer, Waypoint value)
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
}
