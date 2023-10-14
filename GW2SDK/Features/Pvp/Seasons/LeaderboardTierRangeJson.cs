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
            if (max.IsUndefined)
            {
                max.Value = member;
            }
            else if (min.IsUndefined)
            {
                min.Value = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedArrayLength(json.GetArrayLength()));
            }
        }

        return new LeaderboardTierRange
        {
            Maximum = max.GetValue(),
            Minimum = min.GetValue()
        };
    }
}
