using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class LeaderboardEntryJson
{
    public static LeaderboardEntry GetLeaderboardEntry(this in JsonElement json)
    {
        RequiredMember name = "name";
        OptionalMember guildId = "id";
        OptionalMember teamName = "team";
        NullableMember teamId = "team_id";
        RequiredMember rank = "rank";
        RequiredMember date = "date";
        RequiredMember scores = "scores";

        foreach (JsonProperty member in json.EnumerateObject())
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new LeaderboardEntry
        {
            Name = name.Map(static (in value) => value.GetStringRequired()),
            GuildId = guildId.Map(static (in value) => value.GetString()) ?? "",
            TeamName = teamName.Map(static (in value) => value.GetString()) ?? "",
            TeamId = teamId.Map(static (in value) => value.GetInt32()),
            Rank = rank.Map(static (in value) => value.GetInt32()),
            Date = date.Map(static (in value) => value.GetDateTimeOffset()),
            Scores = scores.Map(static (in values) => values.GetList(static (in value) => value.GetScore()))
        };
    }
}
