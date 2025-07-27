using System.Text.Json;
using GuildWars2.Exploration.Maps;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

internal static class MapScoresJson
{
    public static MapScores GetMapScores(this in JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new MapScores
        {
            Kind = type.Map(static (in JsonElement value) => value.GetEnum<MapKind>()),
            Scores = scores.Map(static (in JsonElement value) => value.GetDistribution())
        };
    }
}
