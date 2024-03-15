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
            if (id.Match(member))
            {
                id = member;
            }
            else if (scores.Match(member))
            {
                scores = member;
            }
            else if (victoryPoints.Match(member))
            {
                victoryPoints = member;
            }
            else if (skirmishes.Match(member))
            {
                skirmishes = member;
            }
            else if (maps.Match(member))
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
