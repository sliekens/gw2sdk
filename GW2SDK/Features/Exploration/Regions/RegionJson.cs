﻿using System.Text.Json;
using GuildWars2.Exploration.Maps;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Regions;

internal static class RegionJson
{
    public static Region GetRegion(this in JsonElement json)
    {
        RequiredMember name = "name";
        RequiredMember labelCoordinates = "label_coord";
        RequiredMember continentRectangle = "continent_rect";
        RequiredMember maps = "maps";
        RequiredMember id = "id";
        foreach (var member in json.EnumerateObject())
        {
            if (name.Match(member))
            {
                name = member;
            }
            else if (labelCoordinates.Match(member))
            {
                labelCoordinates = member;
            }
            else if (continentRectangle.Match(member))
            {
                continentRectangle = member;
            }
            else if (maps.Match(member))
            {
                maps = member;
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Region
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            LabelCoordinates = labelCoordinates.Map(static (in JsonElement value) => value.GetCoordinate()),
            ContinentRectangle =
                continentRectangle.Map(static (in JsonElement value) => value.GetContinentRectangle()),
            Maps = maps.Map(static (in JsonElement value) => value.GetMap(static (in JsonElement entry) => entry.GetMap())
                .ToDictionary(kvp => int.Parse(kvp.Key), kvp => kvp.Value)
            )
        };
    }
}
