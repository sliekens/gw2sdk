using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches.Stats;

internal static class MatchStatsJson
{
    public static MatchStats GetMatchStats(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember deaths = "deaths";
        RequiredMember kills = "kills";
        RequiredMember maps = "maps";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (deaths.Match(member))
            {
                deaths = member;
            }
            else if (kills.Match(member))
            {
                kills = member;
            }
            else if (maps.Match(member))
            {
                maps = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MatchStats
        {
            Id = id.Map(value => value.GetStringRequired()),
            Deaths = deaths.Map(value => value.GetDistribution(missingMemberBehavior)),
            Kills = kills.Map(value => value.GetDistribution(missingMemberBehavior)),
            Maps = maps.Map(
                values => values.GetList(value => value.GetMapSummary(missingMemberBehavior))
            )
        };
    }
}
