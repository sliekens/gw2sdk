using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class SeasonJson
{
    public static Season GetSeason(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember start = "start";
        RequiredMember end = "end";
        RequiredMember active = "active";
        RequiredMember divisions = "divisions";
        OptionalMember ranks = "ranks";
        RequiredMember leaderboards = "leaderboards";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == start.Name)
            {
                start = member;
            }
            else if (member.Name == end.Name)
            {
                end = member;
            }
            else if (member.Name == active.Name)
            {
                active = member;
            }
            else if (member.Name == divisions.Name)
            {
                divisions = member;
            }
            else if (member.Name == ranks.Name)
            {
                ranks = member;
            }
            else if (member.Name == leaderboards.Name)
            {
                leaderboards = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Season
        {
            Id = id.Map(value => value.GetStringRequired()),
            Name = name.Map(value => value.GetStringRequired()),
            Start = start.Map(value => value.GetDateTime()),
            End = end.Map(value => value.GetDateTime()),
            Active = active.Map(value => value.GetBoolean()),
            Divisions =
                divisions.Map(
                    values => values.GetList(value => value.GetDivision(missingMemberBehavior))
                ),
            Ranks =
                ranks.Map(
                    values => values.GetList(value => value.GetSeasonRank(missingMemberBehavior))
                ),
            Leaderboards =
                leaderboards.Map(value => value.GetLeaderboardGroup(missingMemberBehavior))
        };
    }
}
