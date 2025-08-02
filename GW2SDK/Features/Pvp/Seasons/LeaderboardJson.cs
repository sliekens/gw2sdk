using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class LeaderboardJson
{
    public static Leaderboard GetLeaderboard(this in JsonElement json)
    {
        RequiredMember settings = "settings";
        RequiredMember scorings = "scorings";

        foreach (var member in json.EnumerateObject())
        {
            if (settings.Match(member))
            {
                settings = member;
            }
            else if (scorings.Match(member))
            {
                scorings = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Leaderboard
        {
            Settings = settings.Map(static (in JsonElement value) => value.GetLeaderboardSetting()),
            Scorings = scorings.Map(static (in JsonElement values) =>
                values.GetList(static (in JsonElement value) => value.GetLeaderboardScoring())
            )
        };
    }
}
