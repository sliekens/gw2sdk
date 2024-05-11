using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches.Scores;

internal static class MatchScoresJson
{
    public static MatchScores GetMatchScores(this JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MatchScores
        {
            Id = id.Map(static value => value.GetStringRequired()),
            Scores = scores.Map(static value => value.GetDistribution()),
            VictoryPoints = victoryPoints.Map(static value => value.GetDistribution()),
            Skirmishes =
                skirmishes.Map(
                    static values => values.GetList(static value => value.GetSkirmish())
                ),
            Maps = maps.Map(static values => values.GetList(static value => value.GetMapSummary()))
        };
    }
}
