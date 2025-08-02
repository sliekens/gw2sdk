using System.Drawing;
using System.Numerics;
using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Wvw.Objectives;

internal static class PointJson
{
    public static PointF GetCoordinateF(this in JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedArrayLength(json.GetArrayLength());
            }
        }

        return new PointF(x.GetSingle(), y.GetSingle());
    }

    public static Vector3 GetCoordinate3(this in JsonElement json)
    {
        JsonElement x = default;
        JsonElement y = default;
        JsonElement z = default;

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
            else if (z.ValueKind == JsonValueKind.Undefined)
            {
                z = entry;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedArrayLength(json.GetArrayLength());
            }
        }

        return new Vector3(x.GetSingle(), y.GetSingle(), z.GetSingle());
    }
}
