using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

[PublicAPI]
public static class MapScoresJson
{
    public static MapScores GetMapScores(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember type = new("type");
        RequiredMember scores = new("scores");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(type.Name))
            {
                type = member;
            }
            else if (member.NameEquals(scores.Name))
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
            Kind = type.Select(value => value.GetEnum<MapKind>(missingMemberBehavior)),
            Scores = scores.Select(value => value.GetDistribution(missingMemberBehavior))
        };
    }
}
