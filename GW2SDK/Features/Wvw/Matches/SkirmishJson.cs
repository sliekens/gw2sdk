using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

internal static class SkirmishJson
{
    public static Skirmish GetSkirmish(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember scores = "scores";
        RequiredMember mapScores = "map_scores";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (scores.Match(member))
            {
                scores = member;
            }
            else if (mapScores.Match(member))
            {
                mapScores = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Skirmish
        {
            Id = id.Map(value => value.GetInt32()),
            Scores = scores.Map(value => value.GetDistribution(missingMemberBehavior)),
            MapScores = mapScores.Map(
                values => values.GetList(value => value.GetMapScores(missingMemberBehavior))
            )
        };
    }
}
