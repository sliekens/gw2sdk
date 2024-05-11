using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class LeaderboardJson
{
    public static Leaderboard GetLeaderboard(this JsonElement json)
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Leaderboard
        {
            Settings = settings.Map(static value => value.GetLeaderboardSetting()),
            Scorings = scorings.Map(
                static values => values.GetList(static value => value.GetLeaderboardScoring())
            )
        };
    }
}
