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
            if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == guildId.Name)
            {
                guildId = member;
            }
            else if (member.Name == teamName.Name)
            {
                teamName = member;
            }
            else if (member.Name == teamId.Name)
            {
                teamId = member;
            }
            else if (member.Name == rank.Name)
            {
                rank = member;
            }
            else if (member.Name == date.Name)
            {
                date = member;
            }
            else if (member.Name == scores.Name)
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
