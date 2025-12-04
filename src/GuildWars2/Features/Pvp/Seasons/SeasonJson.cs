using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class SeasonJson
{
    public static Season GetSeason(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        OptionalMember start = "start";
        OptionalMember end = "end";
        RequiredMember active = "active";
        RequiredMember divisions = "divisions";
        OptionalMember ranks = "ranks";
        RequiredMember leaderboards = "leaderboards";

        foreach (JsonProperty member in json.EnumerateObject())
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
            Id = id.Map(static (in value) => value.GetStringRequired()),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            Start = start.Map(static (in value) => value.GetDateTime()),
            End = end.Map(static (in value) => value.GetDateTime()),
            Active = active.Map(static (in value) => value.GetBoolean()),
            Divisions =
                divisions.Map(static (in values) => values.GetList(static (in value) => value.GetDivision())),
            Ranks = ranks.Map(static (in values) => values.GetList(static (in value) => value.GetSkillBadge())
            ),
            Leaderboards = leaderboards.Map(static (in value) => value.GetLeaderboardGroup())
        };
    }
}
