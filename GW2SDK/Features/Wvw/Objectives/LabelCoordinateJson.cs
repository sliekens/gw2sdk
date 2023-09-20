using System.Drawing;
using System.Text.Json;

namespace GuildWars2.Wvw.Objectives;

[PublicAPI]
public static class LabelCoordinateJson
{
    public static PointF GetLabelCoordinate(
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
