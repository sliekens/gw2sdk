using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class LeaderboardTierRangeJson
{
    public static LeaderboardTierRange GetLeaderboardTierRange(this JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
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
