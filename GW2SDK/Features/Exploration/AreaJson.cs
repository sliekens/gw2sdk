using System.Drawing;
using System.Text.Json;
using JetBrains.Annotations;

namespace GuildWars2.Exploration;

[PublicAPI]
public static class AreaJson
{
    public static Area GetArea(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        // TODO: use MissingMemberBehavior
        var northWest = json[0];
        var northWestX = northWest[0].GetSingle();
        var northWestY = northWest[1].GetSingle();
        var southEast = json[1];
        var southEastX = southEast[0].GetSingle();
        var southEastY = southEast[1].GetSingle();
        return new Area
        {
            NorthWest = new PointF(northWestX, northWestY),
            SouthEast = new PointF(southEastX, southEastY)
        };
    }
}
