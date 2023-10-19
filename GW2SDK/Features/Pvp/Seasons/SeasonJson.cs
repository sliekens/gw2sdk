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
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        RequiredMember start = new("start");
        RequiredMember end = new("end");
        RequiredMember active = new("active");
        RequiredMember divisions = new("divisions");
        OptionalMember ranks = new("ranks");
        RequiredMember leaderboards = new("leaderboards");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(start.Name))
            {
                start.Value = member.Value;
            }
            else if (member.NameEquals(end.Name))
            {
                end.Value = member.Value;
            }
            else if (member.NameEquals(active.Name))
            {
                active.Value = member.Value;
            }
            else if (member.NameEquals(divisions.Name))
            {
                divisions.Value = member.Value;
            }
            else if (member.NameEquals(ranks.Name))
            {
                ranks.Value = member.Value;
            }
            else if (member.NameEquals(leaderboards.Name))
            {
                leaderboards.Value = member.Value;
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
