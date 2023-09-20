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
        RequiredMember<string> name = new("name");
        OptionalMember<string> guildId = new("id");
        OptionalMember<string> teamName = new("team");
        NullableMember<int> teamId = new("team_id");
        RequiredMember<int> rank = new("rank");
        RequiredMember<DateTimeOffset> date = new("date");
        RequiredMember<Score> scores = new("scores");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(guildId.Name))
            {
                guildId.Value = member.Value;
            }
            else if (member.NameEquals(teamName.Name))
            {
                teamName.Value = member.Value;
            }
            else if (member.NameEquals(teamId.Name))
            {
                teamId.Value = member.Value;
            }
            else if (member.NameEquals(rank.Name))
            {
                rank.Value = member.Value;
            }
            else if (member.NameEquals(date.Name))
            {
                date.Value = member.Value;
            }
            else if (member.NameEquals(scores.Name))
            {
                scores.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new LeaderboardEntry
        {
            Name = name.GetValue(),
            GuildId = guildId.GetValueOrEmpty(),
            TeamName = teamName.GetValueOrEmpty(),
            TeamId = teamId.GetValue(),
            Rank = rank.GetValue(),
            Date = date.GetValue(),
            Scores = scores.SelectMany(value => value.GetScore(missingMemberBehavior))
        };
    }
}
