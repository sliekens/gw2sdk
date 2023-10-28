using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class LeaderboardGroupJson
{
    public static LeaderboardGroup GetLeaderboardGroup(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        OptionalMember ladder = "ladder";
        OptionalMember legendary = "legendary";
        OptionalMember guild = "guild";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == ladder.Name)
            {
                ladder = member;
            }
            else if (member.Name == legendary.Name)
            {
                legendary = member;
            }
            else if (member.Name == guild.Name)
            {
                guild = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new LeaderboardGroup
        {
            Ladder = ladder.Map(value => value.GetLeaderboard(missingMemberBehavior)),
            Legendary = legendary.Map(value => value.GetLeaderboard(missingMemberBehavior)),
            Guild = guild.Map(value => value.GetLeaderboard(missingMemberBehavior))
        };
    }
}
