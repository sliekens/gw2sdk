using System;
using System.Collections.Generic;
using System.Text.Json;
using GW2SDK.Exploration.Maps;

namespace GW2SDK.Exploration.Regions;

internal static class RegionMapsReader
{
    public static Dictionary<int, Map> GetRegionMaps(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        Dictionary<int, Map> maps = new();
        foreach (var member in json.EnumerateObject())
        {
            if (int.TryParse(member.Name, out var id))
            {
                maps[id] = member.Value.GetMap(missingMemberBehavior);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return maps;
    }
}
