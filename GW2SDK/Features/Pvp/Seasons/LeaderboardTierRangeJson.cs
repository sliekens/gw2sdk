using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
public static class LeaderboardTierRangeJson
{
    public static LeaderboardTierRange GetLeaderboardTierRange(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<double> min = new("min");
        RequiredMember<double> max = new("max");

        foreach (var member in json.EnumerateArray())
        {
            if (max.IsMissing)
            {
                max.Value = member;
            }
            else if (min.IsMissing)
            {
                min.Value = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(json.GetRawText()));
            }
        }

        return new LeaderboardTierRange
        {
            Maximum = max.GetValue(),
            Minimum = min.GetValue()
        };
    }
}
