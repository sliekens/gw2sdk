using System.Drawing;
using System.Text.Json;

namespace GuildWars2.Exploration;

[PublicAPI]
public static class RectangleJson
{
    public static Rectangle GetMapRectangle(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(
                    Strings.UnexpectedArrayLength(json.GetArrayLength())
                );
            }
        }

        var sw = southWest.GetCoordinate(missingMemberBehavior);
        var ne = northEast.GetCoordinate(missingMemberBehavior);

        return new Rectangle(
            sw.X, // The x-coordinate of the upper-left corner of the rectangle
            ne.Y, // The y-coordinate of the upper-left corner of the rectangle
            ne.X - sw.X,
            sw.Y - ne.Y
        );
    }

    public static Rectangle GetContinentRectangle(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(
                    Strings.UnexpectedArrayLength(json.GetArrayLength())
                );
            }
        }

        var nw = northWest.GetCoordinate(missingMemberBehavior);
        var se = southEast.GetCoordinate(missingMemberBehavior);

        return new Rectangle(
            nw.X, // The x-coordinate of the upper-left corner of the rectangle
            nw.Y, // The y-coordinate of the upper-left corner of the rectangle
            se.X - nw.X,
            se.Y - nw.Y
        );
    }
}
