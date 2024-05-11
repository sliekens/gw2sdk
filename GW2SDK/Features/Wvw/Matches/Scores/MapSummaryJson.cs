using System.Text.Json;
using GuildWars2.Exploration.Maps;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches.Scores;

internal static class MapSummaryJson
{
    public static MapSummary GetMapSummary(this JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MapSummary
        {
            Id = id.Map(static value => value.GetInt32()),
            Kind = type.Map(static value => value.GetEnum<MapKind>()),
            Scores = scores.Map(static value => value.GetDistribution())
        };
    }
}
