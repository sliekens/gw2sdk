using System.Drawing;
using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Exploration;

internal static class RectangleJson
{
    public static Rectangle GetMapRectangle(this in JsonElement json)
    {
        JsonElement southWest = default;
        JsonElement northEast = default;

        foreach (var entry in json.EnumerateArray())
        {
            if (southWest.ValueKind == JsonValueKind.Undefined)
            {
                southWest = entry;
            }
            else if (northEast.ValueKind == JsonValueKind.Undefined)
            {
                northEast = entry;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedArrayLength(json.GetArrayLength());
            }
        }

        var sw = southWest.GetCoordinate();
        var ne = northEast.GetCoordinate();

        return new Rectangle(
            sw.X, // The x-coordinate of the upper-left corner of the rectangle
            ne.Y, // The y-coordinate of the upper-left corner of the rectangle
            ne.X - sw.X,
            sw.Y - ne.Y
        );
    }

    public static Rectangle GetContinentRectangle(this in JsonElement json)
    {
        JsonElement northWest = default;
        JsonElement southEast = default;

        foreach (var entry in json.EnumerateArray())
        {
            if (northWest.ValueKind == JsonValueKind.Undefined)
            {
                northWest = entry;
            }
            else if (southEast.ValueKind == JsonValueKind.Undefined)
            {
                southEast = entry;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedArrayLength(json.GetArrayLength());
            }
        }

        var nw = northWest.GetCoordinate();
        var se = southEast.GetCoordinate();

        return new Rectangle(
            nw.X, // The x-coordinate of the upper-left corner of the rectangle
            nw.Y, // The y-coordinate of the upper-left corner of the rectangle
            se.X - nw.X,
            se.Y - nw.Y
        );
    }
}
