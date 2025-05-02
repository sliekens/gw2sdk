using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class SeasonJson
{
    public static Season GetSeason(this JsonElement json)
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
            if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (start.Match(member))
            {
                start = member;
            }
            else if (end.Match(member))
            {
                end = member;
            }
            else if (active.Match(member))
            {
                active = member;
            }
            else if (divisions.Match(member))
            {
                divisions = member;
            }
            else if (ranks.Match(member))
            {
                ranks = member;
            }
            else if (leaderboards.Match(member))
            {
                leaderboards = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Season
        {
            Id = id.Map(static value => value.GetStringRequired()),
            Name = name.Map(static value => value.GetStringRequired()),
            Start = start.Map(static value => value.GetDateTime()),
            End = end.Map(static value => value.GetDateTime()),
            Active = active.Map(static value => value.GetBoolean()),
            Divisions =
                divisions.Map(static values => values.GetList(static value => value.GetDivision())),
            Ranks = ranks.Map(static values => values.GetList(static value => value.GetSkillBadge())
            ),
            Leaderboards = leaderboards.Map(static value => value.GetLeaderboardGroup())
        };
    }
}
