using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches.Scores;

internal static class MatchScoresJson
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
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == scores.Name)
            {
                scores = member;
            }
            else if (member.Name == victoryPoints.Name)
            {
                victoryPoints = member;
            }
            else if (member.Name == skirmishes.Name)
            {
                skirmishes = member;
            }
            else if (member.Name == maps.Name)
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
            Skirmishes =
                skirmishes.Map(
                    values => values.GetList(value => value.GetSkirmish(missingMemberBehavior))
                ),
            Maps = maps.Map(
                values => values.GetList(value => value.GetMapSummary(missingMemberBehavior))
            )
        };
    }
}
