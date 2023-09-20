using System.Text.Json;
using GuildWars2.Exploration.PointsOfInterest;

namespace GuildWars2.Exploration.Charts;

[PublicAPI]
public static class ChartPointsOfInterestJson
{
    public static Dictionary<int, PointOfInterest> GetChartPointsOfInterest(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        Dictionary<int, PointOfInterest> pointsOfInterest = new();
        foreach (var member in json.EnumerateObject())
        {
            if (int.TryParse(member.Name, out var id))
            {
                pointsOfInterest[id] = member.Value.GetPointOfInterest(missingMemberBehavior);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return pointsOfInterest;
    }
}
