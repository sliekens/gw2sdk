using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class LeaderboardGroupJson
{
    public static LeaderboardGroup GetLeaderboardGroup(this in JsonElement json)
    {
        OptionalMember ladder = "ladder";
        OptionalMember legendary = "legendary";
        OptionalMember guild = "guild";

        foreach (JsonProperty member in json.EnumerateObject())
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
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new LeaderboardGroup
        {
            Ladder = ladder.Map(static (in value) => value.GetLeaderboard()),
            Legendary = legendary.Map(static (in value) => value.GetLeaderboard()),
            Guild = guild.Map(static (in value) => value.GetLeaderboard())
        };
    }
}
