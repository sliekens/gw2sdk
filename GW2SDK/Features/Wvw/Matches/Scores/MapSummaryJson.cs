using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches.Scores;

[PublicAPI]
public static class MapSummaryJson
{
    public static MapSummary GetMapSummary(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember type = new("type");
        RequiredMember scores = new("scores");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(type.Name))
            {
                type.Value = member.Value;
            }
            else if (member.NameEquals(scores.Name))
            {
                scores.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MapSummary
        {
            Id = id.Select(value => value.GetInt32()),
            Kind = type.Select(value => value.GetEnum<MapKind>(missingMemberBehavior)),
            Scores = scores.Select(value => value.GetDistribution(missingMemberBehavior))
        };
    }
}
