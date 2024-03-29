using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Standings;

internal static class BestStandingJson
{
    public static BestStanding GetBestStanding(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember totalPoints = "total_points";
        RequiredMember division = "division";
        RequiredMember tier = "tier";
        RequiredMember points = "points";
        RequiredMember repeats = "repeats";

        foreach (var member in json.EnumerateObject())
        {
            if (totalPoints.Match(member))
            {
                totalPoints = member;
            }
            else if (division.Match(member))
            {
                division = member;
            }
            else if (tier.Match(member))
            {
                tier = member;
            }
            else if (points.Match(member))
            {
                points = member;
            }
            else if (repeats.Match(member))
            {
                repeats = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BestStanding
        {
            TotalPips = totalPoints.Map(value => value.GetInt32()),
            Division = division.Map(value => value.GetInt32()),
            Tier = tier.Map(value => value.GetInt32()),
            Pips = points.Map(value => value.GetInt32()),
            Repeats = repeats.Map(value => value.GetInt32())
        };
    }
}
