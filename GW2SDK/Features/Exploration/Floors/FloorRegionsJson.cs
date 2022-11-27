using System;
using System.Collections.Generic;
using System.Text.Json;
using GW2SDK.Exploration.Regions;
using JetBrains.Annotations;

namespace GW2SDK.Exploration.Floors;

[PublicAPI]
public static class FloorRegionsJson
{
    public static Dictionary<int, Region> GetFloorRegions(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        Dictionary<int, Region> regions = new();
        foreach (var member in json.EnumerateObject())
        {
            if (int.TryParse(member.Name, out var id))
            {
                regions[id] = member.Value.GetRegion(missingMemberBehavior);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return regions;
    }
}
