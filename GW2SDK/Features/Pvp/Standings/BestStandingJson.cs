using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Standings;

[PublicAPI]
public static class BestStandingJson
{
    public static BestStanding GetBestStanding(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember totalPoints = new("total_points");
        RequiredMember division = new("division");
        RequiredMember tier = new("tier");
        RequiredMember points = new("points");
        RequiredMember repeats = new("repeats");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(totalPoints.Name))
            {
                totalPoints.Value = member.Value;
            }
            else if (member.NameEquals(division.Name))
            {
                division.Value = member.Value;
            }
            else if (member.NameEquals(tier.Name))
            {
                tier.Value = member.Value;
            }
            else if (member.NameEquals(points.Name))
            {
                points.Value = member.Value;
            }
            else if (member.NameEquals(repeats.Name))
            {
                repeats.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BestStanding
        {
            TotalPoints = totalPoints.Select(value => value.GetInt32()),
            Division = division.Select(value => value.GetInt32()),
            Tier = tier.Select(value => value.GetInt32()),
            Points = points.Select(value => value.GetInt32()),
            Repeats = repeats.Select(value => value.GetInt32())
        };
    }
}
