﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using GW2SDK.Exploration.PointsOfInterest;
using JetBrains.Annotations;

namespace GW2SDK.Exploration.Charts;

[PublicAPI]
public static class ChartPointsOfInterestReader
{
    public static Dictionary<int, PointOfInterest> GetChartPointsOfInterest(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        Dictionary<int, PointOfInterest> pointsOfInterest = new();
        foreach (var member in json.EnumerateObject())
        {
            if (int.TryParse(member.Name, out var id))
            {
                pointsOfInterest[id] = member.Value.GetPointOfInterest(missingMemberBehavior);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return pointsOfInterest;
    }
}