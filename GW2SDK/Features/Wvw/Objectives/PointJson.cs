using System.Drawing;
using System.Numerics;
using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Objectives;

[PublicAPI]
public static class PointJson
{
    public static PointF GetCoordinateF(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember x = new("[0]");
        RequiredMember y = new("[1]");

        foreach (var entry in json.EnumerateArray())
        {
            if (x.IsUndefined)
            {
                x.Value = entry;
            }
            else if (y.IsUndefined)
            {
                y.Value = entry;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedArrayLength(json.GetArrayLength()));
            }
        }

        return new PointF(
            x.Select(value => value.GetSingle()),
            y.Select(value => value.GetSingle())
        );
    }

    public static Vector3 GetCoordinate3(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember x = new("[0]");
        RequiredMember y = new("[1]");
        RequiredMember z = new("[2]");

        foreach (var entry in json.EnumerateArray())
        {
            if (x.IsUndefined)
            {
                x.Value = entry;
            }
            else if (y.IsUndefined)
            {
                y.Value = entry;
            }
            else if (z.IsUndefined)
            {
                z.Value = entry;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedArrayLength(json.GetArrayLength()));
            }
        }

        return new Vector3(
            x.Select(value => value.GetSingle()),
            y.Select(value => value.GetSingle()),
            z.Select(value => value.GetSingle())
        );
    }
}
