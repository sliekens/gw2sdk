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
        RequiredMember<string> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<DateTime> start = new("start");
        RequiredMember<DateTime> end = new("end");
        RequiredMember<bool> active = new("active");
        RequiredMember<Division> divisions = new("divisions");
        OptionalMember<SeasonRank> ranks = new("ranks");
        RequiredMember<LeaderboardGroup> leaderboards = new("leaderboards");

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
            Id = id.GetValue(),
            Name = name.GetValue(),
            Start = start.Select(value => value.GetDateTime()),
            End = end.Select(value => value.GetDateTime()),
            Active = active.GetValue(),
            Divisions = divisions.SelectMany(value => value.GetDivision(missingMemberBehavior)),
            Ranks = ranks.SelectMany(value => value.GetSeasonRank(missingMemberBehavior)),
            Leaderboards =
                leaderboards.Select(value => value.GetLeaderboardGroup(missingMemberBehavior))
        };
    }
}
