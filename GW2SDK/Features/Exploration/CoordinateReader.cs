﻿using System.Drawing;
using System.Text.Json;
using JetBrains.Annotations;

namespace GW2SDK.Exploration;

[PublicAPI]
public static class CoordinateReader
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
