using System.Drawing;
using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration;

[PublicAPI]
public static class RectangleJson
{
    public static Rectangle GetMapRectangle(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember southWest = new("[0]");
        RequiredMember northEast = new("[1]");

        foreach (var entry in json.EnumerateArray())
        {
            if (southWest.IsUndefined)
            {
                southWest.Value = entry;
            }
            else if (northEast.IsUndefined)
            {
                northEast.Value = entry;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedArrayLength(json.GetArrayLength()));
            }
        }

        var sw = southWest.Select(value => value.GetCoordinate(missingMemberBehavior));
        var ne = northEast.Select(value => value.GetCoordinate(missingMemberBehavior));

        return new Rectangle(
            x: sw.X, // The x-coordinate of the upper-left corner of the rectangle
            y: ne.Y, // The y-coordinate of the upper-left corner of the rectangle
            width: ne.X - sw.X,
            height: sw.Y - ne.Y
        );
    }

    public static Rectangle GetContinentRectangle(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember northWest = new("[0]");
        RequiredMember southEast = new("[1]");

        foreach (var entry in json.EnumerateArray())
        {
            if (northWest.IsUndefined)
            {
                northWest.Value = entry;
            }
            else if (southEast.IsUndefined)
            {
                southEast.Value = entry;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedArrayLength(json.GetArrayLength()));
            }
        }

        var nw = northWest.Select(value => value.GetCoordinate(missingMemberBehavior));
        var se = southEast.Select(value => value.GetCoordinate(missingMemberBehavior));

        return new Rectangle(
            x: nw.X, // The x-coordinate of the upper-left corner of the rectangle
            y: nw.Y, // The y-coordinate of the upper-left corner of the rectangle
            width: se.X - nw.X,
            height: se.Y - nw.Y
        );
    }
}
