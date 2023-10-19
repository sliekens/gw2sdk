using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches.Scores;

[PublicAPI]
public static class MatchScoresJson
{
    public static MatchScores GetMatchScores(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember scores = "scores";
        RequiredMember victoryPoints = "victory_points";
        RequiredMember skirmishes = "skirmishes";
        RequiredMember maps = "maps";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(scores.Name))
            {
                scores = member;
            }
            else if (member.NameEquals(victoryPoints.Name))
            {
                victoryPoints = member;
            }
            else if (member.NameEquals(skirmishes.Name))
            {
                skirmishes = member;
            }
            else if (member.NameEquals(maps.Name))
            {
                maps = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MatchScores
        {
            Id = id.Map(value => value.GetStringRequired()),
            Scores = scores.Map(value => value.GetDistribution(missingMemberBehavior)),
            VictoryPoints =
                victoryPoints.Map(value => value.GetDistribution(missingMemberBehavior)),
            Skirmishes = skirmishes.Map(values => values.GetList(value => value.GetSkirmish(missingMemberBehavior))),
            Maps = maps.Map(values => values.GetList(value => value.GetMapSummary(missingMemberBehavior)))
        };
    }
}
