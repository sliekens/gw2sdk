using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
public static class LeaderboardGroupJson
{
    public static LeaderboardGroup GetLeaderboardGroup(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        OptionalMember<Leaderboard> ladder = new("ladder");
        OptionalMember<Leaderboard> legendary = new("legendary");
        OptionalMember<Leaderboard> guild = new("guild");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(ladder.Name))
            {
                ladder.Value = member.Value;
            }
            else if (member.NameEquals(legendary.Name))
            {
                legendary.Value = member.Value;
            }
            else if (member.NameEquals(guild.Name))
            {
                guild.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new LeaderboardGroup
        {
            Ladder = ladder.Select(value => value.GetLeaderboard(missingMemberBehavior)),
            Legendary = legendary.Select(value => value.GetLeaderboard(missingMemberBehavior)),
            Guild = guild.Select(value => value.GetLeaderboard(missingMemberBehavior))
        };
    }
}
