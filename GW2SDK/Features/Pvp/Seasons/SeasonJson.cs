using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
public static class SeasonJson
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
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(start.Name))
            {
                start = member;
            }
            else if (member.NameEquals(end.Name))
            {
                end = member;
            }
            else if (member.NameEquals(active.Name))
            {
                active = member;
            }
            else if (member.NameEquals(divisions.Name))
            {
                divisions = member;
            }
            else if (member.NameEquals(ranks.Name))
            {
                ranks = member;
            }
            else if (member.NameEquals(leaderboards.Name))
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
            Id = id.Select(value => value.GetStringRequired()),
            Name = name.Select(value => value.GetStringRequired()),
            Start = start.Select(value => value.GetDateTime()),
            End = end.Select(value => value.GetDateTime()),
            Active = active.Select(value => value.GetBoolean()),
            Divisions = divisions.SelectMany(value => value.GetDivision(missingMemberBehavior)),
            Ranks = ranks.SelectMany(value => value.GetSeasonRank(missingMemberBehavior)),
            Leaderboards =
                leaderboards.Select(value => value.GetLeaderboardGroup(missingMemberBehavior))
        };
    }
}
