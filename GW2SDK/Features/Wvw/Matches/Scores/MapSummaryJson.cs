using System.Text.Json;
using GuildWars2.Exploration.Maps;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches.Scores;

internal static class MapSummaryJson
{
    public static MapSummary GetMapSummary(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember type = "type";
        RequiredMember scores = "scores";

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
            else if (scores.Match(member))
            {
                scores = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MapSummary
        {
            Id = id.Map(value => value.GetInt32()),
            Kind = type.Map(value => value.GetEnum<MapKind>(missingMemberBehavior)),
            Scores = scores.Map(value => value.GetDistribution(missingMemberBehavior))
        };
    }
}
