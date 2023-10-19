using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
public static class LeaderboardGroupJson
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
            if (member.NameEquals(ladder.Name))
            {
                ladder = member;
            }
            else if (member.NameEquals(legendary.Name))
            {
                legendary = member;
            }
            else if (member.NameEquals(guild.Name))
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
            Ladder = ladder.Select(value => value.GetLeaderboard(missingMemberBehavior)),
            Legendary = legendary.Select(value => value.GetLeaderboard(missingMemberBehavior)),
            Guild = guild.Select(value => value.GetLeaderboard(missingMemberBehavior))
        };
    }
}
