using System.Text.Json;
using GuildWars2.Exploration.Maps;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

internal static class MapScoresJson
{
    public static MapScores GetMapScores(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember type = "type";
        RequiredMember scores = "scores";

        foreach (var member in json.EnumerateObject())
        {
            if (type.Match(member))
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

        return new MapScores
        {
            Kind = type.Map(value => value.GetEnum<MapKind>()),
            Scores = scores.Map(value => value.GetDistribution(missingMemberBehavior))
        };
    }
}
