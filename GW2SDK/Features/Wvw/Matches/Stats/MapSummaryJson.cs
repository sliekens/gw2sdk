using System.Text.Json;
using GuildWars2.Exploration.Maps;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches.Stats;

internal static class MapSummaryJson
{
    public static MapSummary GetMapSummary(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember type = "type";
        RequiredMember deaths = "deaths";
        RequiredMember kills = "kills";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (type.Match(member))
            {
                type = member;
            }
            else if (deaths.Match(member))
            {
                deaths = member;
            }
            else if (kills.Match(member))
            {
                kills = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MapSummary
        {
            Id = id.Map(value => value.GetInt32()),
            Kind = type.Map(value => value.GetEnum<MapKind>()),
            Deaths = deaths.Map(value => value.GetDistribution(missingMemberBehavior)),
            Kills = kills.Map(value => value.GetDistribution(missingMemberBehavior))
        };
    }
}
