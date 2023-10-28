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
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == deaths.Name)
            {
                deaths = member;
            }
            else if (member.Name == kills.Name)
            {
                kills = member;
            }
            else if (member.Name == maps.Name)
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
