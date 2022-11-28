﻿using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Exploration.MasteryPoints;

[PublicAPI]
public static class MasteryPointJson
{
    public static MasteryPoint GetMasteryPoint(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<double> coordinates = new("coord");
        RequiredMember<int> id = new("id");
        RequiredMember<MasteryRegionName> region = new("region");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(coordinates.Name))
            {
                coordinates.Value = member.Value;
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(region.Name))
            {
                region.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MasteryPoint
        {
            Id = id.GetValue(),
            Coordinates = coordinates.SelectMany(value => value.GetDouble()),
            Region = region.GetValue(missingMemberBehavior)
        };
    }
}
