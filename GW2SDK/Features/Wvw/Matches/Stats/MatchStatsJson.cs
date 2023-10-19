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
        RequiredMember id = new("id");
        RequiredMember deaths = new("deaths");
        RequiredMember kills = new("kills");
        RequiredMember maps = new("maps");

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
            Id = id.Select(value => value.GetStringRequired()),
            Deaths = deaths.Select(value => value.GetDistribution(missingMemberBehavior)),
            Kills = kills.Select(value => value.GetDistribution(missingMemberBehavior)),
            Maps = maps.SelectMany(value => value.GetMapSummary(missingMemberBehavior))
        };
    }
}
