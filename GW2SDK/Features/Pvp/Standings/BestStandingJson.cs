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
        RequiredMember<int> totalPoints = new("total_points");
        RequiredMember<int> division = new("division");
        RequiredMember<int> tier = new("tier");
        RequiredMember<int> points = new("points");
        RequiredMember<int> repeats = new("repeats");

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
            TotalPoints = totalPoints.GetValue(),
            Division = division.GetValue(),
            Tier = tier.GetValue(),
            Points = points.GetValue(),
            Repeats = repeats.GetValue()
        };
    }
}
