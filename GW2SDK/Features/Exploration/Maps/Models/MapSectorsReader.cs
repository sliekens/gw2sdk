using System;
using System.Collections.Generic;
using System.Text.Json;
using GW2SDK.Exploration.Sectors;
using JetBrains.Annotations;

namespace GW2SDK.Exploration.Maps;

[PublicAPI]
public static class MapSectorsReader
{
    public static Dictionary<int, MapSector> GetMapSectors(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        Dictionary<int, MapSector> sectors = new();
        foreach (var member in json.EnumerateObject())
        {
            if (int.TryParse(member.Name, out var id))
            {
                sectors[id] = member.Value.GetMapSector(missingMemberBehavior);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return sectors;
    }
}
