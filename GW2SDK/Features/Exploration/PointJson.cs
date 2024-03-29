﻿using System.Drawing;
using System.Text.Json;

namespace GuildWars2.Exploration;

internal static class PointJson
{
    public static Point GetCoordinate(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        JsonElement x = default;
        JsonElement y = default;

        foreach (var entry in json.EnumerateArray())
        {
            if (x.ValueKind == JsonValueKind.Undefined)
            {
                x = entry;
            }
            else if (y.ValueKind == JsonValueKind.Undefined)
            {
                y = entry;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(
                    Strings.UnexpectedArrayLength(json.GetArrayLength())
                );
            }
        }

        return new Point(x.GetInt32(), y.GetInt32());
    }

    public static PointF GetCoordinateF(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        JsonElement x = default;
        JsonElement y = default;

        foreach (var entry in json.EnumerateArray())
        {
            if (x.ValueKind == JsonValueKind.Undefined)
            {
                x = entry;
            }
            else if (y.ValueKind == JsonValueKind.Undefined)
            {
                y = entry;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(
                    Strings.UnexpectedArrayLength(json.GetArrayLength())
                );
            }
        }

        return new PointF(x.GetSingle(), y.GetSingle());
    }
}
