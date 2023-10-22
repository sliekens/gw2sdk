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
            if (member.NameEquals(totalPoints.Name))
            {
                totalPoints = member;
            }
            else if (member.NameEquals(division.Name))
            {
                division = member;
            }
            else if (member.NameEquals(tier.Name))
            {
                tier = member;
            }
            else if (member.NameEquals(points.Name))
            {
                points = member;
            }
            else if (member.NameEquals(repeats.Name))
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
            TotalPoints = totalPoints.Map(value => value.GetInt32()),
            Division = division.Map(value => value.GetInt32()),
            Tier = tier.Map(value => value.GetInt32()),
            Points = points.Map(value => value.GetInt32()),
            Repeats = repeats.Map(value => value.GetInt32())
        };
    }
}
