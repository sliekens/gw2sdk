﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Maps;

[PublicAPI]
public static class MapSummaryJson
{
    public static MapSummary GetMapSummary(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        RequiredMember minLevel = new("min_level");
        RequiredMember maxLevel = new("max_level");
        RequiredMember defaultFloor = new("default_floor");
        RequiredMember kind = new("type");
        RequiredMember floors = new("floors");
        NullableMember regionId = new("region_id");
        OptionalMember regionName = new("region_name");
        NullableMember continentId = new("continent_id");
        OptionalMember continentName = new("continent_name");
        RequiredMember mapRectangle = new("map_rect");
        RequiredMember continentRectangle = new("continent_rect");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(minLevel.Name))
            {
                minLevel = member;
            }
            else if (member.NameEquals(maxLevel.Name))
            {
                maxLevel = member;
            }
            else if (member.NameEquals(defaultFloor.Name))
            {
                defaultFloor = member;
            }
            else if (member.NameEquals(kind.Name))
            {
                kind = member;
            }
            else if (member.NameEquals(floors.Name))
            {
                floors = member;
            }
            else if (member.NameEquals(regionId.Name))
            {
                regionId = member;
            }
            else if (member.NameEquals(regionName.Name))
            {
                regionName = member;
            }
            else if (member.NameEquals(continentId.Name))
            {
                continentId = member;
            }
            else if (member.NameEquals(continentName.Name))
            {
                continentName = member;
            }
            else if (member.NameEquals(mapRectangle.Name))
            {
                mapRectangle = member;
            }
            else if (member.NameEquals(continentRectangle.Name))
            {
                continentRectangle = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MapSummary
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            MinLevel = minLevel.Select(value => value.GetInt32()),
            MaxLevel = maxLevel.Select(value => value.GetInt32()),
            DefaultFloor = defaultFloor.Select(value => value.GetInt32()),
            Kind = kind.Select(value => value.GetEnum<MapKind>(missingMemberBehavior)),
            Floors = floors.SelectMany(value => value.GetInt32()),
            RegionId = regionId.Select(value => value.GetInt32()),
            RegionName = regionName.Select(value => value.GetString()) ?? "",
            ContinentId = continentId.Select(value => value.GetInt32()),
            ContinentName = continentName.Select(value => value.GetString()) ?? "",
            MapRectangle = mapRectangle.Select(value => value.GetMapRectangle(missingMemberBehavior)),
            ContinentRectangle =
                continentRectangle.Select(value => value.GetContinentRectangle(missingMemberBehavior))
        };
    }
}
