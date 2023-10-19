﻿using System.Text.Json;
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
        RequiredMember id = new("id");
        RequiredMember scores = new("scores");
        RequiredMember mapScores = new("map_scores");

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
            Id = id.Select(value => value.GetInt32()),
            Scores = scores.Select(value => value.GetDistribution(missingMemberBehavior)),
            MapScores = mapScores.SelectMany(value => value.GetMapScores(missingMemberBehavior))
        };
    }
}
