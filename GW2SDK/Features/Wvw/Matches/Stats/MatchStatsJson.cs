using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches.Stats;

[PublicAPI]
public static class MatchStatsJson
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
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(deaths.Name))
            {
                deaths = member;
            }
            else if (member.NameEquals(kills.Name))
            {
                kills = member;
            }
            else if (member.NameEquals(maps.Name))
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
