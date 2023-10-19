using System.Text.Json;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
public static class LeaderboardTierRangeJson
{
    public static LeaderboardTierRange GetLeaderboardTierRange(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        JsonElement min = default;
        JsonElement max = default;

        foreach (var member in json.EnumerateArray())
        {
            if (max.ValueKind == JsonValueKind.Undefined)
            {
                max = member;
            }
            else if (min.ValueKind == JsonValueKind.Undefined)
            {
                min = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(
                    Strings.UnexpectedArrayLength(json.GetArrayLength())
                );
            }
        }

        return new LeaderboardTierRange
        {
            Maximum = max.GetDouble(),
            Minimum = min.GetDouble()
        };
    }
}
