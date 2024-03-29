﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class LeaderboardJson
{
    public static Leaderboard GetLeaderboard(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Leaderboard
        {
            Settings =
                settings.Map(value => value.GetLeaderboardSetting(missingMemberBehavior)),
            Scorings = scorings.Map(
                values => values.GetList(
                    value => value.GetLeaderboardScoring(missingMemberBehavior)
                )
            )
        };
    }
}
