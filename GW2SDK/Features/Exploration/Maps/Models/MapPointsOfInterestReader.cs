using System;
using System.Collections.Generic;
using System.Text.Json;
using JetBrains.Annotations;

namespace GW2SDK.Exploration.Maps;

[PublicAPI]
public static class MapPointsOfInterestReader
{
    public static Dictionary<int, PointOfInterest> GetMapPointsOfInterest(
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
