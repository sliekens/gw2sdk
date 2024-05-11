using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class LeaderboardGroupJson
{
    public static LeaderboardGroup GetLeaderboardGroup(this JsonElement json)
    {
        OptionalMember ladder = "ladder";
        OptionalMember legendary = "legendary";
        OptionalMember guild = "guild";

        foreach (var member in json.EnumerateObject())
        {
            if (ladder.Match(member))
            {
                ladder = member;
            }
            else if (legendary.Match(member))
            {
                legendary = member;
            }
            else if (guild.Match(member))
            {
                guild = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new LeaderboardGroup
        {
            Ladder = ladder.Map(static value => value.GetLeaderboard()),
            Legendary = legendary.Map(static value => value.GetLeaderboard()),
            Guild = guild.Map(static value => value.GetLeaderboard())
        };
    }
}
