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
        RequiredMember name = new("name");
        OptionalMember guildId = new("id");
        OptionalMember teamName = new("team");
        NullableMember teamId = new("team_id");
        RequiredMember rank = new("rank");
        RequiredMember date = new("date");
        RequiredMember scores = new("scores");

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
            Scores = scores.SelectMany(value => value.GetScore(missingMemberBehavior))
        };
    }
}
