using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

[PublicAPI]
public static class SkirmishJson
{
    public static Skirmish GetSkirmish(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<Distribution> scores = new("scores");
        RequiredMember<MapScores> mapScores = new("map_scores");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(scores.Name))
            {
                scores.Value = member.Value;
            }
            else if (member.NameEquals(mapScores.Name))
            {
                mapScores.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Skirmish
        {
            Id = id.GetValue(),
            Scores = scores.Select(value => value.GetDistribution(missingMemberBehavior)),
            MapScores = mapScores.SelectMany(value => value.GetMapScores(missingMemberBehavior))
        };
    }
}
