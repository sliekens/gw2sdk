using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pvp.Standings;

internal static class BestStandingJson
{
    public static BestStanding GetBestStanding(this in JsonElement json)
    {
        RequiredMember totalPoints = "total_points";
        RequiredMember division = "division";
        RequiredMember tier = "tier";
        RequiredMember points = "points";
        RequiredMember repeats = "repeats";

        foreach (JsonProperty member in json.EnumerateObject())
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new BestStanding
        {
            TotalPips = totalPoints.Map(static (in JsonElement value) => value.GetInt32()),
            Division = division.Map(static (in JsonElement value) => value.GetInt32()),
            Tier = tier.Map(static (in JsonElement value) => value.GetInt32()),
            Pips = points.Map(static (in JsonElement value) => value.GetInt32()),
            Repeats = repeats.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
