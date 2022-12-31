using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Wvw.Matches.Scores;

[PublicAPI]
public static class MatchScoresJson
{
    public static MatchScores GetMatchScores(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> id = new("id");
        RequiredMember<Distribution> scores = new("scores");
        RequiredMember<Distribution> victoryPoints = new("victory_points");
        RequiredMember<Skirmish> skirmishes = new("skirmishes");
        RequiredMember<MapSummary> maps = new("maps");

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
            else if (member.NameEquals(victoryPoints.Name))
            {
                victoryPoints.Value = member.Value;
            }
            else if (member.NameEquals(skirmishes.Name))
            {
                skirmishes.Value = member.Value;
            }
            else if (member.NameEquals(maps.Name))
            {
                maps.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MatchScores
        {
            Id = id.GetValue(),
            Scores = scores.Select(value => value.GetDistribution(missingMemberBehavior)),
            VictoryPoints =
                victoryPoints.Select(value => value.GetDistribution(missingMemberBehavior)),
            Skirmishes = skirmishes.SelectMany(value => value.GetSkirmish(missingMemberBehavior)),
            Maps = maps.SelectMany(value => value.GetMapSummary(missingMemberBehavior))
        };
    }
}
