using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class LeaderboardEntryJson
{
    public static LeaderboardEntry GetLeaderboardEntry(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = "name";
        OptionalMember guildId = "id";
        OptionalMember teamName = "team";
        NullableMember teamId = "team_id";
        RequiredMember rank = "rank";
        RequiredMember date = "date";
        RequiredMember scores = "scores";

        foreach (var member in json.EnumerateObject())
        {
            if (name.Match(member))
            {
                name = member;
            }
            else if (guildId.Match(member))
            {
                guildId = member;
            }
            else if (teamName.Match(member))
            {
                teamName = member;
            }
            else if (teamId.Match(member))
            {
                teamId = member;
            }
            else if (rank.Match(member))
            {
                rank = member;
            }
            else if (date.Match(member))
            {
                date = member;
            }
            else if (scores.Match(member))
            {
                scores = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new LeaderboardEntry
        {
            Name = name.Map(value => value.GetStringRequired()),
            GuildId = guildId.Map(value => value.GetString()) ?? "",
            TeamName = teamName.Map(value => value.GetString()) ?? "",
            TeamId = teamId.Map(value => value.GetInt32()),
            Rank = rank.Map(value => value.GetInt32()),
            Date = date.Map(value => value.GetDateTimeOffset()),
            Scores = scores.Map(
                values => values.GetList(value => value.GetScore(missingMemberBehavior))
            )
        };
    }
}
