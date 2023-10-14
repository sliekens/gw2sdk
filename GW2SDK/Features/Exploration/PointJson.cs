using System.Drawing;
using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration;

[PublicAPI]
public static class PointJson
{
    public static Point GetCoordinate(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> x = new("[0]");
        RequiredMember<int> y = new("[1]");

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

        return new Point(
            x.Select(value => value.GetInt32()),
            y.Select(value => value.GetInt32())
        );
    }

    public static PointF GetCoordinateF(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<float> x = new("[0]");
        RequiredMember<float> y = new("[1]");

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
}
