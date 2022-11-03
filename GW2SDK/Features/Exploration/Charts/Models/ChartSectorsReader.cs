﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using GW2SDK.Exploration.Sectors;
using JetBrains.Annotations;

namespace GW2SDK.Exploration.Charts;

[PublicAPI]
public static class ChartSectorsReader
{
    public static Dictionary<int, Sector> GetChartSectors(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        Dictionary<int, Sector> sectors = new();
        foreach (var member in json.EnumerateObject())
        {
            if (int.TryParse(member.Name, out var id))
            {
                sectors[id] = member.Value.GetSector(missingMemberBehavior);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return sectors;
    }
}