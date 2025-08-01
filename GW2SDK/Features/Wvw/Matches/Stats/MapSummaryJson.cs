﻿using System.Text.Json;
using GuildWars2.Exploration.Maps;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches.Stats;

internal static class MapSummaryJson
{
    public static MapSummary GetMapSummary(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember type = "type";
        RequiredMember deaths = "deaths";
        RequiredMember kills = "kills";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (type.Match(member))
            {
                type = member;
            }
            else if (deaths.Match(member))
            {
                deaths = member;
            }
            else if (kills.Match(member))
            {
                kills = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new MapSummary
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Kind = type.Map(static (in JsonElement value) => value.GetEnum<MapKind>()),
            Deaths = deaths.Map(static (in JsonElement value) => value.GetDistribution()),
            Kills = kills.Map(static (in JsonElement value) => value.GetDistribution())
        };
    }
}
