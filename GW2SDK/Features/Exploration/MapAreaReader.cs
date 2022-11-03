using System.Drawing;
using System.Text.Json;
using JetBrains.Annotations;

namespace GW2SDK.Exploration;

[PublicAPI]
public static class MapAreaReader
{
    public static MapArea GetMapArea(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        // TODO: use MissingMemberBehavior
        var southWest = json[0];
        var southWestX = southWest[0].GetSingle();
        var southWestY = southWest[1].GetSingle();
        var northEast = json[1];
        var northEastX = northEast[0].GetSingle();
        var northEastY = northEast[1].GetSingle();
        return new MapArea
        {
            SouthWest = new PointF(southWestX, southWestY),
            NorthEast = new PointF(northEastX, northEastY)
        };
    }
}
