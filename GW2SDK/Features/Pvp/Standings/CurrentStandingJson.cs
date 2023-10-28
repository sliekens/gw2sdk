using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Standings;

internal static class CurrentStandingJson
{
    public static CurrentStanding GetCurrentStanding(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember totalPoints = "total_points";
        RequiredMember division = "division";
        RequiredMember tier = "tier";
        RequiredMember points = "points";
        RequiredMember repeats = "repeats";
        NullableMember rating = "rating";
        NullableMember decay = "decay";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == totalPoints.Name)
            {
                totalPoints = member;
            }
            else if (member.Name == division.Name)
            {
                division = member;
            }
            else if (member.Name == tier.Name)
            {
                tier = member;
            }
            else if (member.Name == points.Name)
            {
                points = member;
            }
            else if (member.Name == repeats.Name)
            {
                repeats = member;
            }
            else if (member.Name == rating.Name)
            {
                rating = member;
            }
            else if (member.Name == decay.Name)
            {
                decay = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CurrentStanding
        {
            TotalPoints = totalPoints.Map(value => value.GetInt32()),
            Division = division.Map(value => value.GetInt32()),
            Tier = tier.Map(value => value.GetInt32()),
            Points = points.Map(value => value.GetInt32()),
            Repeats = repeats.Map(value => value.GetInt32()),
            Rating = rating.Map(value => value.GetInt32()),
            Decay = decay.Map(value => value.GetInt32())
        };
    }
}
