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
        RequiredMember<string> id = new("id");
        RequiredMember<Distribution> deaths = new("deaths");
        RequiredMember<Distribution> kills = new("kills");
        RequiredMember<MapSummary> maps = new("maps");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(deaths.Name))
            {
                deaths.Value = member.Value;
            }
            else if (member.NameEquals(kills.Name))
            {
                kills.Value = member.Value;
            }
            else if (member.NameEquals(maps.Name))
            {
                maps.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MatchStats
        {
            Id = id.GetValue(),
            Deaths = deaths.Select(value => value.GetDistribution(missingMemberBehavior)),
            Kills = kills.Select(value => value.GetDistribution(missingMemberBehavior)),
            Maps = maps.SelectMany(value => value.GetMapSummary(missingMemberBehavior))
        };
    }
}
