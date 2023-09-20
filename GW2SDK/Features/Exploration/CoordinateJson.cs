using System.Drawing;
using System.Text.Json;

namespace GuildWars2.Exploration;

[PublicAPI]
public static class CoordinateJson
{
    public static PointF GetCoordinate(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        // TODO: use MissingMemberBehavior
        var x = json[0].GetSingle();
        var y = json[1].GetSingle();
        return new PointF(x, y);
    }
}
