using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
public static class LeaderboardEntryJson
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
            if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(guildId.Name))
            {
                guildId = member;
            }
            else if (member.NameEquals(teamName.Name))
            {
                teamName = member;
            }
            else if (member.NameEquals(teamId.Name))
            {
                teamId = member;
            }
            else if (member.NameEquals(rank.Name))
            {
                rank = member;
            }
            else if (member.NameEquals(date.Name))
            {
                date = member;
            }
            else if (member.NameEquals(scores.Name))
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
            Name = name.Select(value => value.GetStringRequired()),
            GuildId = guildId.Select(value => value.GetString()) ?? "",
            TeamName = teamName.Select(value => value.GetString()) ?? "",
            TeamId = teamId.Select(value => value.GetInt32()),
            Rank = rank.Select(value => value.GetInt32()),
            Date = date.Select(value => value.GetDateTimeOffset()),
            Scores = scores.Select(values => values.GetList(value => value.GetScore(missingMemberBehavior)))
        };
    }
}
