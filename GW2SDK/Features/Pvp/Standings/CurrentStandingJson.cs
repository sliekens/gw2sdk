using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Standings;

internal static class CurrentStandingJson
{
    public static CurrentStanding GetCurrentStanding(this JsonElement json)
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
            else if (rating.Match(member))
            {
                rating = member;
            }
            else if (decay.Match(member))
            {
                decay = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CurrentStanding
        {
            TotalPips = totalPoints.Map(static value => value.GetInt32()),
            Division = division.Map(static value => value.GetInt32()),
            Tier = tier.Map(static value => value.GetInt32()),
            Pips = points.Map(static value => value.GetInt32()),
            Repeats = repeats.Map(static value => value.GetInt32()),
            Rating = rating.Map(static value => value.GetInt32()),
            Decay = decay.Map(static value => value.GetInt32())
        };
    }
}
