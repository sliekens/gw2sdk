﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches.Scores;

internal static class MapSummaryJson
{
    public static MapSummary GetMapSummary(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember type = "type";
        RequiredMember scores = "scores";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == type.Name)
            {
                type = member;
            }
            else if (member.Name == scores.Name)
            {
                scores = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MapSummary
        {
            Id = id.Map(value => value.GetInt32()),
            Kind = type.Map(value => value.GetEnum<MapKind>(missingMemberBehavior)),
            Scores = scores.Map(value => value.GetDistribution(missingMemberBehavior))
        };
    }
}
