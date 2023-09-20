using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
public static class LeaderboardJson
{
    public static Leaderboard GetLeaderboard(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<LeaderboardSetting> settings = new("settings");
        RequiredMember<LeaderboardScoring> scorings = new("scorings");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(settings.Name))
            {
                settings.Value = member.Value;
            }
            else if (member.NameEquals(scorings.Name))
            {
                scorings.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Leaderboard
        {
            Settings =
                settings.Select(value => value.GetLeaderboardSetting(missingMemberBehavior)),
            Scorings = scorings.SelectMany(
                value => value.GetLeaderboardScoring(missingMemberBehavior)
            )
        };
    }
}
